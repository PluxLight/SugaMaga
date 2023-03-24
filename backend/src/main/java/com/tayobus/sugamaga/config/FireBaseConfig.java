package com.tayobus.sugamaga.config;

import com.google.auth.oauth2.GoogleCredentials;
import com.google.firebase.FirebaseApp;
import com.google.firebase.FirebaseOptions;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.context.annotation.Configuration;

import javax.annotation.PostConstruct;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.nio.file.Paths;

@Configuration
public class FireBaseConfig {
    private final Logger logger = LoggerFactory.getLogger(FireBaseConfig.class);

    private final String fileDirectory = Paths.get("").toAbsolutePath() + "";

    @PostConstruct
    public void init() throws FileNotFoundException {
        logger.info("fileDirectory : " + fileDirectory);
        String ubuntuPath = "/var/jenkins_home/workspace/SugaMaga_BE@2/backend";
        String os = System.getProperty("os.name").toLowerCase();
        FileInputStream serviceAccount = null;

        try{
            if (os.contains("win")) {
                serviceAccount =
                        new FileInputStream(fileDirectory +
                                "/src/main/resources/serviceAccountKey.json");
            }
            else {
                serviceAccount =
                        new FileInputStream(ubuntuPath +
                                "/src/main/resources/serviceAccountKey.json");
            }
            FirebaseOptions options = new FirebaseOptions.Builder()
                    .setCredentials(GoogleCredentials.fromStream(serviceAccount))
                    .build();
            FirebaseApp.initializeApp(options);

            logger.info("firebase app name : " + FirebaseApp.getInstance().getName());
        }catch (Exception e){
            e.printStackTrace();
        }
    }
}
