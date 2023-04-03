FROM adoptopenjdk/openjdk11
WORKDIR /usr/app
COPY ${JAR_FILE} app.jar
EXPOSE 18081
CMD ["java", "-jar", "code_meets.jar"]
