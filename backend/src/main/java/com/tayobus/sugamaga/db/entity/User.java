package com.tayobus.sugamaga.db.entity;

import io.swagger.annotations.ApiModelProperty;
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
@Table(name = "user")
@DynamicInsert
public class User {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @JoinColumn(name = "user_idx")
    private Long userIdx;

    @Column(unique = true)
    @ApiModelProperty(value="유저 이메일", example = "user001")
    private String email;

    @Column(unique = true)
    @ApiModelProperty(value="유저 UID", example = "userUID123")
    private String uid;

    @ApiModelProperty(value="유저 닉네임", example = "유저001")
    private String nickname;

    @Column(columnDefinition = "int default 1")
    @ApiModelProperty(value="유저 활성화 상태", example = "1")
    private int active;
}
