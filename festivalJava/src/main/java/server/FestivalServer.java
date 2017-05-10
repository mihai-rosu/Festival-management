package server;

import model.Artist;
import model.Bilet;
import model.Spectacol;
import model.User;
import network.services.ComException;
import network.services.IClient;
import network.services.IServer;
import repository.ArtistJdbcRepository;
import repository.BiletJdbcRepository;
import repository.SpectacolJdbcRepository;
import repository.UserJdbcRepository;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

/**
 * Created by Mihai on 25.04.2017.
 */
public class FestivalServer implements IServer {
    UserJdbcRepository userRepo;
    ArtistJdbcRepository artistRepo;
    BiletJdbcRepository biletRepo;
    SpectacolJdbcRepository spectacolRepo;
    private Map<String, IClient> loggedClients;

    public FestivalServer(UserJdbcRepository userRepo, ArtistJdbcRepository artistRepo, BiletJdbcRepository biletRepo, SpectacolJdbcRepository spectacolRepo) {
        this.userRepo = userRepo;
        this.artistRepo = artistRepo;
        this.biletRepo = biletRepo;
        this.spectacolRepo = spectacolRepo;
        this.loggedClients = new ConcurrentHashMap<>();
    }

    public synchronized void login(User user, IClient client) throws ComException {
        User userR = userRepo.findOne(user.getId());
        if (userR != null || !userR.getPassword().equals(user.getPassword())) {
            if (loggedClients.get(user.getId()) != null)
                throw new ComException("User already logged in.");
            loggedClients.put(user.getId(), client);
        } else
            throw new ComException("Authentication failed.");
    }

    public synchronized void logout(User user, IClient client) throws ComException {
        IClient localClient = loggedClients.remove(user.getId());
        if (localClient == null)
            throw new ComException("User " + user.getId() + " is not logged in.");
    }

    public synchronized List<Spectacol> getAllSpectacole() {
        List<Spectacol> lista = new ArrayList<>();
        Iterable<Spectacol> list = spectacolRepo.findAll();
        for (Spectacol spec : list) {
            Artist a = artistRepo.findOne(spec.getIdA());
            spec.setNumeArtist(a.getNume());
            lista.add(spec);
        }
        return lista;
    }

    public synchronized Integer getNextIDBilet() {
        return biletRepo.size() + 1;
    }

    public synchronized Bilet saveBilet(Bilet b) {
        Integer idS = b.getIdS();
        Spectacol s = spectacolRepo.findOne(idS);
        if (s.getLocuridisponibile() - b.getCantitate() < 0)
            return null;
        else {
            s.setLocuridisponibile(s.getLocuridisponibile() - b.getCantitate());
            s.setLocuriocupate(s.getLocuriocupate() + b.getCantitate());
            spectacolRepo.delete(idS);
            spectacolRepo.save(s);
            biletRepo.save(b);
            return b;
        }
    }

    public synchronized List<Spectacol> getSpectacole(String data) {
        List<Spectacol> lista = new ArrayList<Spectacol>();
        Iterable<Spectacol> allSpectacole = spectacolRepo.findAll();
        for (Spectacol s : allSpectacole)
            if (s.getDate().equals(data)) {
                Artist a = artistRepo.findOne(s.getIdA());
                s.setNumeArtist(a.getNume());
                lista.add(s);
            }

        return lista;
    }

//    private final int defaultThreadsNo=5;
//    private void notifyUpdate() throws ComException {
//
//
//        ExecutorService executor= Executors.newFixedThreadPool(defaultThreadsNo);
//        for(User us :(List<User>)loggedClients){
//            IClient chatClient=loggedClients.get(us.getId());
//            if (chatClient!=null)
//                executor.execute(() -> {
//                    try {
//                        chatClient.partAdded();
//                    } catch (ComException e) {
//                        System.err.println(e.getMessage());
//                    }
//                });
//        }
//
//        executor.shutdown();
//    }

}
