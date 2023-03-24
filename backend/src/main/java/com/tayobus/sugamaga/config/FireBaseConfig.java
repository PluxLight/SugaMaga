package com.tayobus.sugamaga.config;

import com.google.auth.oauth2.GoogleCredentials;
import com.google.firebase.FirebaseApp;
import com.google.firebase.FirebaseOptions;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.context.annotation.Configuration;

import javax.annotation.PostConstruct;
import java.io.FileInputStream;
import java.nio.file.Paths;

@Configuration
public class FireBaseConfig {
    private final Logger logger = LoggerFactory.getLogger(FireBaseConfig.class);

    private final String fileDirectory = Paths.get("").toAbsolutePath() + "";
    @PostConstruct
    public void init(){
        logger.info("fileDirectory : " + fileDirectory);
        logger.info("fileDirectory + : " + fileDirectory + "/src/main/resources/serviceAccountKey.json");
        logger.info("file paths : " + this.getClass().getResource("").getPath());

        try{
            FileInputStream serviceAccount =
                    new FileInputStream(fileDirectory + "src/main/resources/serviceAccountKey.json");
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
