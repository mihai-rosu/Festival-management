package network.rpcprotocol;

import model.Bilet;
import model.Spectacol;
import model.User;
import network.services.ComException;
import network.services.IClient;
import network.services.IServer;

import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import java.net.Socket;
import java.util.List;

/**
 * Created by omg_s on 10-Apr-17.
 */
public class ClientRpcReflectionWorker implements Runnable, IClient{
    private IServer server;
    private Socket connection;

    private ObjectInputStream input;
    private ObjectOutputStream output;
    private volatile boolean connected;

    public ClientRpcReflectionWorker(IServer server, Socket connection){
        this.server = server;
        this.connection = connection;
        try{
            output = new ObjectOutputStream(connection.getOutputStream());
            output.flush();
            input = new ObjectInputStream(connection.getInputStream());
            connected = true;
        }catch (IOException e){
            e.printStackTrace();
        }
    }

    public void run(){
        while(connected){
            try {
                Object request=input.readObject();
                Response response=handleRequest((Request)request);
                if (response!=null){
                    sendResponse(response);
                }
            } catch (IOException e) {
                e.printStackTrace();
            } catch (ClassNotFoundException e) {
                e.printStackTrace();
            }
            try {
                Thread.sleep(1000);
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
        }
        try {
            input.close();
            output.close();
            connection.close();
        } catch (IOException e) {
            System.out.println("Error "+e);
        }
    }

    private static Response okResponse=new Response.Builder().type(ResponseType.OK).build();

    private Response handleRequest(Request request){
        Response response = null;
        String handlerName = "handle" + (request).type();
        System.out.println("Nume handler: " + handlerName);
        try{
            Method method = this.getClass().getDeclaredMethod(handlerName, Request.class);
            response = (Response)method.invoke(this,request);
            System.out.println("metoda " + handlerName + " invocata");
        }catch (NoSuchMethodException e) {
        e.printStackTrace();
        } catch (InvocationTargetException e) {
        e.printStackTrace();
        } catch (IllegalAccessException e) {
        e.printStackTrace();
        }

        return response;
    }

    private Response handleLOGIN(Request request){
        System.out.println("Login request ..."+request.type());
        User user = (User)request.data();

        try {
            server.login(user, this);
            return okResponse;
        } catch (ComException e) {
            connected=false;
            return new Response.Builder().type(ResponseType.ERROR).data(e.getMessage()).build();
        }
    }

    private Response handleLOGOUT(Request request){
        System.out.println("Logout request...");
        User user = (User)request.data();;
        try {
            server.logout(user, this);
            connected=false;
            return okResponse;

        } catch (ComException e) {
            return new Response.Builder().type(ResponseType.ERROR).data(e.getMessage()).build();
        }
    }

    private Response handleGETALLSPEC(Request request){
        try {
            List<Spectacol> participanti = server.getAllSpectacole();
            return new Response.Builder().type(ResponseType.GETALLSPEC).data(participanti).build();
        }catch(ComException e){
            return new Response.Builder().type(ResponseType.ERROR).data(e.getMessage()).build();

        }
    }

    private Response handleGETNEXTID(Request request){
        try {
            Integer id = server.getNextIDBilet();
            return new Response.Builder().type(ResponseType.GETALLSPEC).data(id).build();
        }catch(ComException e){
            return new Response.Builder().type(ResponseType.ERROR).data(e.getMessage()).build();

        }
    }

    private Response handleGETSPEC(Request request){
        try {
            String data = (String)request.data();
            System.out.println(data);
            List<Spectacol> specs = server.getSpectacole(data);
            return new Response.Builder().type(ResponseType.GETSPEC).data(specs).build();
        }catch(ComException e){
            return new Response.Builder().type(ResponseType.ERROR).data(e.getMessage()).build();

        }
    }



    private Response handleADDBILET (Request request){
        try {
            Bilet bilet = (Bilet) request.data();
            server.saveBilet(bilet);
            return okResponse;
        } catch(ComException e){
            return new Response.Builder().type(ResponseType.ERROR).data(e.getMessage()).build();

        }
    }



    private void sendResponse(Response response) throws IOException{
        System.out.println("sending response "+response);
        output.writeObject(response);
        output.flush();
    }

    public void biletAdded() throws ComException {
        Response resp = new Response.Builder().type(ResponseType.UPDATE).build();
        try{
            sendResponse(resp);
        }catch(IOException e){
            e.printStackTrace();
        }
    }
}
