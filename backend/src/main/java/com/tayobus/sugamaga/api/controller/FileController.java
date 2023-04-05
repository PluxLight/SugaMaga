package com.tayobus.sugamaga.api.controller;


import com.tayobus.sugamaga.api.service.FileService;
import com.tayobus.sugamaga.db.entity.Images;
import io.swagger.annotations.Api;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.Parameter;
import io.swagger.v3.oas.annotations.media.Content;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.core.io.InputStreamResource;
import org.springframework.core.io.Resource;
import org.springframework.http.*;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.multipart.MultipartFile;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.ArrayList;
import java.util.List;

@RestController
@RequestMapping("/api/file")
@Api(tags = "파일 API")
public class FileController {
    private final Logger logger = LoggerFactory.getLogger(FileController.class);

    private final FileService fileService;

    @Autowired
    public FileController(FileService fileService) {
        this.fileService = fileService;
    }

    @Operation(summary = "이미지 확인 및 다운로드", description = "서버에 올라온 이미지 확인 or 다운로드 "
            + " \n img src 태그에 값을 넣어서 확인바랍니다")
    @GetMapping("/images/{fileName}")
    public ResponseEntity<Resource> getImageFile(@PathVariable String fileName) throws FileNotFoundException {
        String path = "./images/" + fileName;
        logger.info("filePath - " + path);

        InputStreamResource inputStreamResource = new InputStreamResource(new FileInputStream(path));

        return ResponseEntity
                .ok()
                .contentType(MediaType.APPLICATION_OCTET_STREAM)
                .body(inputStreamResource);
    }

    @Operation(summary = "이미지 파일 업로드", description = "이미지 파일을 서버에 업로드")
    @PostMapping(value="/images", consumes = "multipart/form-data")
    public ResponseEntity<?> uploadImageFiles(
            @Parameter(
                    description = "Files to be uploaded",
                    content = @Content(mediaType = MediaType.MULTIPART_FORM_DATA_VALUE)
            )
            @RequestPart (value = "files", required = true) MultipartFile[] multipartFiles,
            @RequestParam("files") List<MultipartFile> files) {
        String oriFileName = null;

        for (MultipartFile multipartFile : files) {
            oriFileName = multipartFile.getOriginalFilename();
            String filePath = "./images/" + oriFileName;
            logger.info("파일 저장 위치 - " + filePath);

            try (FileOutputStream writer = new FileOutputStream(filePath)) {
                writer.write(multipartFile.getBytes());
            } catch (Exception e) {
                logger.info(e.getMessage(), e);

                return new ResponseEntity<String>("FAIL", HttpStatus.INTERNAL_SERVER_ERROR);
            }
        }

        return new ResponseEntity<String>(oriFileName, HttpStatus.OK);
    }

    @Operation(summary = "이미지 파일 리스트 조회", description = "찾을 이미지 리스트의 Key 입력")
    @GetMapping(value="/images")
    public ResponseEntity<?> getImageList(@RequestParam String imagesKey) {
        logger.info("get images List");

        try {
            List<Images> imagesList = new ArrayList<>();
            imagesList = fileService.getImagesList(imagesKey);

            logger.info(imagesList.toString());

            return new ResponseEntity<>(imagesList, HttpStatus.OK);
        } catch (Exception e) {
            logger.info(e.toString());

            return new ResponseEntity<>("Fail", HttpStatus.BAD_REQUEST);
        }
    }
    
    

    @GetMapping("/download")
    public ResponseEntity<?> download(@RequestParam String fileName) {
        String path = "./release_file/" + fileName;
        logger.info("file path - " + path);

        try {
            Path filePath = Paths.get(path);
            Resource resource = new InputStreamResource(Files.newInputStream(filePath)); // 파일 resource 얻기

            File file = new File(path);
            String contentType = Files.probeContentType(filePath);

            HttpHeaders headers = new HttpHeaders();
            headers.add(HttpHeaders.CONTENT_TYPE, contentType);

            // 다운로드 되거나 로컬에 저장되는 용도로 쓰이는지를 알려주는 헤더
            headers
                    .setContentDisposition(
                            ContentDisposition.builder("attachment")
                                    .filename(file.getName()).build());

            return new ResponseEntity<>(resource, headers, HttpStatus.OK);
        } catch(Exception e) {
            logger.warn("file download error - ", e);

            return new ResponseEntity<Object>(null, HttpStatus.CONFLICT);
        }
    }
}
