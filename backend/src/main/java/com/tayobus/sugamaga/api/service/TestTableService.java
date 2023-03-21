package com.tayobus.sugamaga.api.service;


import com.tayobus.sugamaga.api.request.TestTableRequest;
import com.tayobus.sugamaga.db.entity.TestTable;
import com.tayobus.sugamaga.db.repository.TestTableRepository;
import lombok.RequiredArgsConstructor;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
@RequiredArgsConstructor
public class TestTableService {
    private final Logger logger = LoggerFactory.getLogger(TestTableService.class);
    private final TestTableRepository testTableRepository;

    public List<TestTable> testGet(TestTableRequest request) throws RuntimeException {
        try {
            List<TestTable> testTable = testTableRepository.findAllByIntCol(request.getIntCol());

            return testTable;
        } catch (Exception e) {
            logger.warn("testGet fail - " + e);

            return null;
        }
    }

    public String testPost(TestTableRequest request) {
        try {
            testTableRepository.save(TestTable.builder()
                    .intCol(request.getIntCol())
                    .strCol(request.getStrCol())
                    .build());

            return "Success";
        } catch (Exception e) {
            logger.warn("testPost fail - " + e);

            return "Fail";
        }
    }

}
