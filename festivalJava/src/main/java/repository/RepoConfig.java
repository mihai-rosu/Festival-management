package repository;

import model.Spectacol;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.context.annotation.Primary;
import repository.ArtistJdbcRepository;

import java.io.IOException;
import java.util.Properties;


@Configuration
public class RepoConfig {
    @Bean
    @Primary
    public Properties jdbsProps() {
        Properties serverProps = new Properties();
        try {
            serverProps.load(getClass().getResourceAsStream("/bd.config"));
            System.out.println("Properties set. ");
            serverProps.list(System.out);
        } catch (IOException e) {
            System.out.println("Cannot find bd.config " + e);

        }

        return serverProps;

    }

    @Bean(name = "artistRepo")
    public ArtistJdbcRepository createArtistRepository(Properties jdbcProps) {

        return new ArtistJdbcRepository(jdbcProps);
    }

    @Bean(name = "biletRepo")
    public BiletJdbcRepository createBiletRepository(Properties jdbcProps) {

        return new BiletJdbcRepository(jdbcProps);
    }

    @Bean(name = "spectacolRepo")
    public SpectacolJdbcRepository createSpectacolRepository(Properties jdbcProps) {

        return new SpectacolJdbcRepository(jdbcProps);
    }
    @Bean(name = "userRepo")
    public UserJdbcRepository createUserRepository(Properties jdbcProps) {

        return new UserJdbcRepository(jdbcProps);
    }
}
