package server;

import network.Utils.AbstractServer;
import network.Utils.RpcConcurrentServer;
import network.Utils.ServerException;
import network.services.IServer;
import repository.ArtistJdbcRepository;
import repository.BiletJdbcRepository;
import repository.SpectacolJdbcRepository;
import repository.UserJdbcRepository;

import java.io.IOException;
import java.util.Properties;

/**
 * Created by Mihai on 25.04.2017.
 */
public class ServerMain {
    private static int defaultPort=55555;
    public static void main(String[] args) {
        // UserRepository userRepo=new UserRepositoryMock();
        Properties serverProps=new Properties();
        try {
            serverProps.load(ServerMain.class.getResourceAsStream("/bd.config"));
            System.out.println("Server properties set. ");
            serverProps.list(System.out);
        } catch (IOException e) {
            System.err.println("Cannot find chatserver.properties "+e);
            return;
        }
        UserJdbcRepository userRepo=new UserJdbcRepository(serverProps);
        ArtistJdbcRepository artistRepo = new ArtistJdbcRepository(serverProps);
        BiletJdbcRepository biletRepo = new BiletJdbcRepository(serverProps);
        SpectacolJdbcRepository spectacolRepo = new SpectacolJdbcRepository(serverProps);

        IServer festivalServer=new FestivalServer(userRepo,artistRepo,biletRepo,spectacolRepo);
        int chatServerPort=defaultPort;
        try {
            chatServerPort = Integer.parseInt(serverProps.getProperty("server.port"));
        }catch (NumberFormatException nef){
            System.err.println("Wrong  Port Number"+nef.getMessage());
            System.err.println("Using default port "+defaultPort);
        }
        System.out.println("Starting server on port: "+chatServerPort);
        AbstractServer server = new RpcConcurrentServer(chatServerPort, festivalServer);
        try {
            server.start();
        } catch (ServerException e) {
            System.err.println("Error starting the server" + e.getMessage());
        }
    }
}
