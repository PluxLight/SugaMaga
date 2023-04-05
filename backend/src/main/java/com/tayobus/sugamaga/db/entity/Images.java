package com.tayobus.sugamaga.db.entity;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import javax.persistence.Entity;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.Table;

@Data
@NoArgsConstructor
@AllArgsConstructor
@Entity
@Builder
@Table(name = "images")
public class Images {
    @Id
    @JoinColumn(name = "images_idx")
    private int imagesIdx;

    @JoinColumn(name = "images_key")
    private String imagesKey;

    @JoinColumn(name = "images_order")
    private int imagesOrder;

    @JoinColumn(name = "images_name")
    private String imagesName;
}
