package network.Utils;

import java.net.Socket;

/**
 * Created by omg_s on 04-Apr-17.
 */
public abstract class AbsConcurrentServer extends AbstractServer {

    public AbsConcurrentServer(int port) {
        super(port);
        System.out.println("Concurrent AbstractServer");
    }

    protected void processRequest(Socket client) {
        Thread tw=createWorker(client);
        tw.start();
    }

    protected abstract Thread createWorker(Socket client) ;


}
