package network.rpcprotocol;

import model.Bilet;
import model.Spectacol;
import model.User;
import network.services.ComException;
import network.services.IClient;
import network.services.IServer;
import org.omg.PortableInterceptor.INACTIVE;

import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.net.Socket;
import java.util.List;
import java.util.concurrent.BlockingQueue;
import java.util.concurrent.LinkedBlockingQueue;

/**
 * Created by omg_s on 23-Apr-17.
 */
public class ServerRpcProxy implements IServer {
    private String host;
    private int port;

    private IClient client;

    private ObjectInputStream input;
    private ObjectOutputStream output;
    private Socket connection;

    private BlockingQueue<Response> qresponses;
    private volatile boolean finished;

    public ServerRpcProxy(String host, int port) {
        this.host = host;
        this.port = port;
        qresponses = new LinkedBlockingQueue<Response>();
    }

    public void login(User user, IClient client) throws ComException {
        initializeConnection();

        Request req = new Request.Builder().type(RequestType.LOGIN).data(user).build();
        sendRequest(req);
        Response response = readResponse();
        if (response.type() == ResponseType.OK) {
            this.client = client;
            return;
        }
        if (response.type() == ResponseType.ERROR) {
            String err = response.data().toString();
            closeConnection();
            throw new ComException(err);
        }
    }

    public void logout(User user, IClient client) throws ComException {
        Request req = new Request.Builder().type(RequestType.LOGOUT).data(user).build();
        sendRequest(req);
        Response response = readResponse();
        closeConnection();
        if (response.type() == ResponseType.ERROR) {
            String err = response.data().toString();
            throw new ComException(err);
        }
    }

    public List<Spectacol> getAllSpectacole() throws ComException {
        Request req = new Request.Builder().type(RequestType.GETALLSPEC).build();
        sendRequest(req);
        Response response = readResponse();
        if (response.type() == ResponseType.ERROR) {
            String err = response.data().toString();
            throw new ComException(err);
        }
        List<Spectacol> spec = (List<Spectacol>) response.data();
        return spec;
    }

    public List<Spectacol> getSpectacole(String data) throws ComException {
        Request req = new Request.Builder().type(RequestType.GETSPEC).data(data).build();
        sendRequest(req);
        Response response = readResponse();
        if (response.type() == ResponseType.ERROR) {
            String err = response.data().toString();
            throw new ComException(err);
        }
        List<Spectacol> spec = (List<Spectacol>) response.data();
        return spec;
    }

    public Bilet saveBilet(Bilet bilet) throws ComException{
        Request req = new Request.Builder().type(RequestType.ADDBILET).data(bilet).build();
        sendRequest(req);
        Response response = readResponse();
        if(response.type() == ResponseType.ERROR){
            String err = response.data().toString();
            throw new ComException(err);
        }
        Bilet b = (Bilet)response.data();
        return b;
    }

    public Integer getNextIDBilet() throws ComException{
        Request req = new Request.Builder().type(RequestType.GETNEXTID).build();
        sendRequest(req);
        Response response = readResponse();
        if (response.type() == ResponseType.ERROR) {
            String err = response.data().toString();
            throw new ComException(err);
        }
        Integer id = (Integer) response.data();
        return id;
    }

    private void initializeConnection() throws ComException {
        try {
            connection = new Socket(host, port);
            output = new ObjectOutputStream(connection.getOutputStream());
            output.flush();
            input = new ObjectInputStream(connection.getInputStream());
            finished = false;
            startReader();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    private void startReader() {
        Thread tw = new Thread(new ReaderThread());
        tw.start();
    }

    private void closeConnection() {
        finished = true;
        try {
            input.close();
            output.close();
            connection.close();
            client = null;
        } catch (IOException e) {
            e.printStackTrace();
        }

    }

    private void sendRequest(Request request) throws ComException {
        try {
            output.writeObject(request);
            output.flush();
        } catch (IOException e) {
            throw new ComException("Error sending object " + e);
        }

    }

    private Response readResponse() throws ComException {
        Response response = null;
        try {
            /*synchronized (responses){
                responses.wait();
            }
            response = responses.remove(0);    */
            response = qresponses.take();

        } catch (InterruptedException e) {
            e.printStackTrace();
        }
        return response;
    }

    public void handleUpdate(Response response) {

        if (response.type() == ResponseType.UPDATE) {
            try {
                client.biletAdded();
            } catch (ComException e) {
                e.printStackTrace();
            }
        }
    }

    private boolean isUpdate(Response response) {

        return response.type() == ResponseType.UPDATE;
    }

    private class ReaderThread implements Runnable {
        public void run() {
            while (!finished) {
                try {
                    Object response = input.readObject();
                    System.out.println("response received " + response);
                    if (isUpdate((Response) response)) {
                        handleUpdate((Response) response);
                    } else {

                        try {
                            qresponses.put((Response) response);
                        } catch (InterruptedException e) {
                            e.printStackTrace();
                        }
                    }
                } catch (IOException e) {
                    System.out.println("Reading error " + e);
                } catch (ClassNotFoundException e) {
                    System.out.println("Reading error " + e);
                }
            }
        }
    }
}
