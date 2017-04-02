package model;


public class Bilet implements HasId<Integer> {
    private Integer id;
    private Integer idS;
    private String cumparator;
    private int cantitate;

    public Bilet(Integer id, Integer idS, String cumparator, int cantitate) {
        this.id = id;
        this.idS = idS;
        this.cumparator = cumparator;
        this.cantitate = cantitate;
    }

    @Override
    public Integer getId() {
        return id;
    }

    @Override
    public void setId(Integer id) {
        this.id = id;
    }

    public Integer getIdS() {
        return idS;
    }

    public void setIdS(Integer idS) {
        this.idS = idS;
    }

    public String getCumparator() {
        return cumparator;
    }

    public void setCumparator(String cumparator) {
        this.cumparator = cumparator;
    }

    public int getCantitate() {
        return cantitate;
    }

    public void setCantitate(int cantitate) {
        this.cantitate = cantitate;
    }
}