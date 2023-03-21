package com.tayobus.sugamaga.api.request;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@NoArgsConstructor
@AllArgsConstructor
@Getter
@Setter
public final class TestTableRequest {
    private int intCol;
    private String strCol;
}