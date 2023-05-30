package com.tayobus.sugamaga.db.entity;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;
import org.hibernate.annotations.DynamicInsert;

import javax.persistence.*;

@Data
@NoArgsConstructor
@AllArgsConstructor
@Entity
@Builder
@Table(name = "user_custom")
@DynamicInsert
public class UserCustom {
    
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @JoinColumn(name = "user_custom_idx")
    private int userCustomIdx;

    @Column(nullable = false)
    private String uid;

    @Column(nullable = false)
    private int body;

    @Column(nullable = false)
    private int ac;

    @Column(nullable = false)
    private int back;

    @Column(nullable = false)
    private int eye;

    @Column(nullable = false)
    private int hair;

    @Column(nullable = false)
    private int hat;

    @Column(nullable = false)
    private int head;

    @Column(nullable = false)
    private int mouth;

    @Column(nullable = false)
    private int eyebrow;

}
