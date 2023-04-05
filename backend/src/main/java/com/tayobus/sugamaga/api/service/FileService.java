package com.tayobus.sugamaga.api.service;


import com.tayobus.sugamaga.db.entity.Images;
import com.tayobus.sugamaga.db.repository.ImagesRepository;
import lombok.RequiredArgsConstructor;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
@RequiredArgsConstructor
public class FileService {
    private final Logger logger = LoggerFactory.getLogger(FileService.class);
    private final ImagesRepository imagesRepository;

    public List<Images> getImagesList(String imagesKey) {
        logger.info("get image list");

        return imagesRepository.findAllByImagesKeyOrderByImagesOrder(imagesKey);
    }

}
