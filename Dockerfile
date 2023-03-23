FROM adoptopenjdk/openjdk11:latest
VOLUME /tmp
EXPOSE 18081
ARG JAR_FILE=build/libs/ssafy-web-project-1.0-SNAPSHOT.jar
COPY ${JAR_FILE} app.jar
ENTRYPOINT ["java", "-jar", "/app.jar"]


