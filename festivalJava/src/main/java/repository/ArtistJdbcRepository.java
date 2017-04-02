package repository;

import model.Artist;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;
import java.util.Properties;

/**
 * Created by Mihai on 15.03.2017.
 */
public class ArtistJdbcRepository implements IRepository<Integer, Artist> {
    private JdbcUtils dbUtils;

    public ArtistJdbcRepository(Properties props){
        dbUtils=new JdbcUtils(props);
    }

    @Override
    public int size() {
        Connection con=dbUtils.getConnection();
        try(PreparedStatement preStmt=con.prepareStatement("select count(*) as [SIZE] from Artist")) {
            try(ResultSet result = preStmt.executeQuery()) {
                if (result.next()) {
                    return result.getInt("SIZE");
                }
            }
        }catch(SQLException ex){
            System.out.println("Error DB "+ex);
        }
        return 0;
    }

    @Override
    public void save(Artist entity) {
        Connection con=dbUtils.getConnection();
        try(PreparedStatement preStmt=con.prepareStatement("insert into Artist values (?,?)")){
            preStmt.setInt(1,entity.getId());
            preStmt.setString(2,entity.getNume());
            int result=preStmt.executeUpdate();
        }catch (SQLException ex){
            System.out.println("Error DB "+ex);
        }

    }

    @Override
    public void delete(Integer id) {
        Connection con=dbUtils.getConnection();
        try(PreparedStatement preStmt=con.prepareStatement("delete from Artist where id=?")){
            preStmt.setInt(1,id);
            int result=preStmt.executeUpdate();
        }catch (SQLException ex){
            System.out.println("Error DB "+ex);
        }
    }


    @Override
    public Artist findOne(Integer id) {
        Connection con=dbUtils.getConnection();

        try(PreparedStatement preStmt=con.prepareStatement("select * from Artist where id=?")){
            preStmt.setInt(1,id);
            try(ResultSet result=preStmt.executeQuery()) {
                if (result.next()) {
                    Integer idA = result.getInt("id");
                    String nume = result.getString("nume");
                    Artist artist = new Artist(idA,nume);
                    return artist;
                }
            }
        }catch (SQLException ex){
            System.out.println("Error DB "+ex);
        }
        return null;
    }

    @Override
    public Iterable<Artist> findAll() {
        Connection con=dbUtils.getConnection();
        List<Artist> artisti=new ArrayList<>();
        try(PreparedStatement preStmt=con.prepareStatement("select * from Artist")) {
            try(ResultSet result=preStmt.executeQuery()) {
                while (result.next()) {
                    int id = result.getInt("id");
                    String nume = result.getString("nume");
                    Artist artist = new Artist(id,nume);
                    artisti.add(artist);
                }
            }
        } catch (SQLException e) {
            System.out.println("Error DB "+e);
        }
        return artisti;
    }
}
