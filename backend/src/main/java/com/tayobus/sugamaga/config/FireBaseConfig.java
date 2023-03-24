package com.tayobus.sugamaga.config;

import com.google.auth.oauth2.GoogleCredentials;
import com.google.firebase.FirebaseApp;
import com.google.firebase.FirebaseOptions;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.context.annotation.Configuration;
import org.springframework.core.io.ClassPathResource;
import org.springframework.core.io.Resource;

import javax.annotation.PostConstruct;
import java.io.*;

@Configuration
public class FireBaseConfig {
    private final Logger logger = LoggerFactory.getLogger(FireBaseConfig.class);

    @PostConstruct
    public void init() throws IOException {
        Resource resource = new ClassPathResource("serviceAccountKey.json");
        InputStream is = resource.getInputStream();

        File jsonFile = File.createTempFile("serviceAccountKey", ".json");

        try (FileOutputStream fos = new FileOutputStream(jsonFile)) {
            int read;
            byte[] bytes = new byte[1024];

            while ((read = is.read(bytes)) != -1) {
                fos.write(bytes, 0, read);
            }
        }

        try{
            FileInputStream serviceAccount =
                    new FileInputStream(jsonFile);

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
