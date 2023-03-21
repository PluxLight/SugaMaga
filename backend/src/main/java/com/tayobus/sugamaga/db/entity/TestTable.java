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
@Table(name = "test_table")
@DynamicInsert
public class TestTable {
    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private int pk;

    @ApiModelProperty(value="정수 값", example = "1")
    @JoinColumn(name = "int_col")
    private int intCol;

    @ApiModelProperty(value="문자열 값", example = "text")
    @JoinColumn(name = "str_col")
    private String strCol;
}
