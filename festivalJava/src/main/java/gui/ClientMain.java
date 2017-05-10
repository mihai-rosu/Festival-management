package gui;

import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.layout.AnchorPane;
import javafx.stage.Stage;
import network.rpcprotocol.ServerRpcProxy;
import network.services.IServer;
import service.FestivalService;

import java.io.FileReader;
import java.io.IOException;
import java.util.Properties;

/**
 * Created by Mihai on 26.04.2017.
 */
public class ClientMain extends Application {
    private static int defaultChatPort = 55555;
    private static String defaultServer = "localhost";
    @Override
    public void start(Stage stage) throws Exception{
        Properties serverProps=new Properties();
        try {
            serverProps.load(new FileReader("bd.config"));

            serverProps.list(System.out);
        } catch (IOException e) {

            System.out.println("Cannot find config "+e);
            return;
        }
        String serverIP=serverProps.getProperty("server.host",defaultServer);

        int serverPort=defaultChatPort;
        try{
            serverPort=Integer.parseInt(serverProps.getProperty("server.port"));
        }catch(NumberFormatException ex){
            System.err.println("Wrong port number "+ex.getMessage());
            System.out.println("Using default port: "+defaultChatPort);
        }


        IServer server = new ServerRpcProxy(serverIP,serverPort);
        FestivalService service = new FestivalService(server);

        FXMLLoader loader = new FXMLLoader();
        loader.setLocation(ClientMain.class.getResource("/Views/loginWindow.fxml"));
        Parent root=null;

        try {
            root = loader.load();
        } catch (IOException e) {
            e.printStackTrace();
        }

        LoginViewController ctrl = loader.getController();
        ctrl.setService(service);

        Scene scene = new Scene(root);
        stage.setScene(scene);
        stage.setTitle("Login");
        stage.show();
    }

    public static void main(String[] a){
        launch(a);
    }
}
