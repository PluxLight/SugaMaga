package com.tayobus.sugamaga.config;

import com.google.auth.oauth2.GoogleCredentials;
import com.google.firebase.FirebaseApp;
import com.google.firebase.FirebaseOptions;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.context.annotation.Configuration;
import org.springframework.core.io.ClassPathResource;

import javax.annotation.PostConstruct;
import java.io.File;
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
        ClassPathResource cpr = new ClassPathResource("serviceAccountKey.json");

        File dir = new File(fileDirectory);
        File files[] = dir.listFiles();

        for (int i = 0; i < files.length; i++) {
            logger.info("file: " + files[i]);
        }

        String path = System.getProperty("user.dir");
        logger.info("path : " + path);

        try{
            FileInputStream serviceAccount =
                    new FileInputStream(cpr.getFile());
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
