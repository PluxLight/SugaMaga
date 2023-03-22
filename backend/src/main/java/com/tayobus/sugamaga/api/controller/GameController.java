package com.tayobus.sugamaga.api.controller;

import com.tayobus.sugamaga.api.service.GameService;
import com.tayobus.sugamaga.db.entity.DropTable;
import io.swagger.annotations.Api;
import io.swagger.v3.oas.annotations.Operation;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

@RestController
@RequestMapping("/api/game")
@Api(tags = "게임 API")
public class GameController {
    private final Logger logger = LoggerFactory.getLogger(GameController.class);

    @Autowired
    private GameService ganeService;

    @Operation(summary = "드랍 테이블 조회", description = "drop table get")
    @GetMapping("/droptable")
    public ResponseEntity<?> getDropTable(@RequestParam int tableIdx) {
        logger.info("get drop table");

        try {
            List<DropTable> dropTableList = new ArrayList<>();
            if (tableIdx == 0) {
                dropTableList = ganeService.getDropTable();
            }
            else {
                dropTableList = ganeService.getTargetDropTable(tableIdx);
            }

            Map<String, Object> result = new HashMap<>();
            result.put("result", dropTableList);

            return new ResponseEntity<>(result, HttpStatus.OK);
        } catch (Exception e) {
            logger.info(e.toString());

            return new ResponseEntity<>("Fail", HttpStatus.BAD_REQUEST);
        }
    }



}
