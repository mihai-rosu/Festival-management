package network.Utils;

/**
 * Created by omg_s on 04-Apr-17.
 */
public class ServerException extends Exception{
    public ServerException() {
        super();
    }

    public ServerException(String message) {
        super(message);
    }

    public ServerException(String message, Throwable cause) {
        super(message, cause);
    }
}