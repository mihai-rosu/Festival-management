package repository;

/**
 * Created by grigo on 11/14/16.
 */
public interface IRepository<ID, T> {
    int size();
    void save(T entity);
    void delete(ID id);
    T findOne(ID id);
    Iterable<T> findAll();
}