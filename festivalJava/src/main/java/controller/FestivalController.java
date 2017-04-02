package controller;

import model.Artist;
import model.Bilet;
import model.Spectacol;
import repository.ArtistJdbcRepository;
import repository.BiletJdbcRepository;
import repository.SpectacolJdbcRepository;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by rmihai on 28.03.2017.
 */
public class FestivalController {
    ArtistJdbcRepository artistRepo;
    BiletJdbcRepository biletRepo;
    SpectacolJdbcRepository spectacolRepo;

    public FestivalController(ArtistJdbcRepository artistRepo, BiletJdbcRepository biletRepo, SpectacolJdbcRepository spectacolRepo) {
        this.artistRepo = artistRepo;
        this.biletRepo = biletRepo;
        this.spectacolRepo = spectacolRepo;
    }

    public List<Spectacol> getAllSpectacole()
    {
        List<Spectacol> lista=new ArrayList<>();
        Iterable<Spectacol> list=spectacolRepo.findAll();
        for (Spectacol spec:list) {
            Artist a = artistRepo.findOne(spec.getIdA());
            spec.setNumeArtist(a.getNume());
            lista.add(spec);
        }
        return lista;
    }

    public Integer getNextIDBilet(){
        return biletRepo.size()+1;
    }

    public Bilet saveBilet(Bilet b)
    {
        Integer idS=b.getIdS();
        Spectacol s=spectacolRepo.findOne(idS);
        if(s.getLocuridisponibile()-b.getCantitate()<0)
            return null;
        else{
            s.setLocuridisponibile(s.getLocuridisponibile()-b.getCantitate());
            s.setLocuriocupate(s.getLocuriocupate()+b.getCantitate());
            spectacolRepo.delete(idS);
            spectacolRepo.save(s);
            biletRepo.save(b);
            return b;
        }
    }

    public List<Spectacol> getSpectacole(String data)
    {
        List<Spectacol> lista=new ArrayList<Spectacol>();
        Iterable<Spectacol> allSpectacole=spectacolRepo.findAll();
        for (Spectacol s:allSpectacole)
            if(s.getDate().equals(data)){
                Artist a = artistRepo.findOne(s.getIdA());
                s.setNumeArtist(a.getNume());
                lista.add(s);}

        return lista;
    }
}
