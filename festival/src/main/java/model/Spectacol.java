package model;


public class Spectacol implements HasId<Integer> {
    private Integer id;
    private Integer idA;
    private String numeArtist;
    private String date;
    private String ora;
    private String locatie;
    private Integer locuridisponibile;
    private Integer locuriocupate;

    public Spectacol(Integer id, Integer idA, String date, String ora, String locatie, Integer locuridisponibile, Integer locuriocupate) {
        this.id = id;
        this.idA = idA;
        this.date = date;
        this.ora = ora;
        this.locatie = locatie;
        this.locuridisponibile = locuridisponibile;
        this.locuriocupate = locuriocupate;
    }

    @Override
    public Integer getId() {
        return id;
    }

    @Override
    public void setId(Integer id) {
        this.id = id;
    }

    public String getOra() {
        return ora;
    }

    public void setOra(String ora) {
        this.ora = ora;
    }

    public String getNumeArtist() {
        return numeArtist;
    }

    public void setNumeArtist(String numeArtist) {
        this.numeArtist = numeArtist;
    }

    public Integer getIdA() {
        return idA;
    }

    public void setIdA(Integer idA) {
        this.idA = idA;
    }

    public String getDate() {
        return date;
    }

    public void setDate(String date) {
        this.date = date;
    }

    public String getLocatie() {
        return locatie;
    }

    public void setLocatie(String locatie) {
        this.locatie = locatie;
    }

    public Integer getLocuridisponibile() {
        return locuridisponibile;
    }

    public void setLocuridisponibile(Integer locuridisponibile) {
        this.locuridisponibile = locuridisponibile;
    }

    public Integer getLocuriocupate() {
        return locuriocupate;
    }

    public void setLocuriocupate(Integer locuriocupate) {
        this.locuriocupate = locuriocupate;
    }
}
