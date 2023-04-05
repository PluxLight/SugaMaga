package com.tayobus.sugamaga.db.repository;

import com.tayobus.sugamaga.db.entity.Images;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.List;

public interface ImagesRepository extends JpaRepository<Images, Integer> {
    List<Images> findAllByImagesKeyOrderByImagesOrder(String imagesKey);
}
