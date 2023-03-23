FROM adoptopenjdk/openjdk11:latest
VOLUME /tmp
EXPOSE 18081
ARG JAR_FILE=build/libs/sugamaga-0.0.1-SNAPSHOT.jar

COPY ${JAR_FILE} app.jar
ENTRYPOINT ["java", "-jar", "/app.jar"]

