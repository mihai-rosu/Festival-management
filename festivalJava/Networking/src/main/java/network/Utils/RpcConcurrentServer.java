package network.Utils;

import network.rpcprotocol.ClientRpcReflectionWorker;
import network.services.IServer;

import java.net.Socket;

/**
 * Created by omg_s on 10-Apr-17.
 */
public class RpcConcurrentServer extends AbsConcurrentServer {
    private IServer chatServer;
    public RpcConcurrentServer(int port, IServer chatServer) {
        super(port);
        this.chatServer = chatServer;
        System.out.println("Chat- ChatRpcConcurrentServer");
    }

    @Override
    protected Thread createWorker(Socket client) {

        ClientRpcReflectionWorker worker=new ClientRpcReflectionWorker(chatServer, client);

        Thread tw=new Thread(worker);
        return tw;
    }
}