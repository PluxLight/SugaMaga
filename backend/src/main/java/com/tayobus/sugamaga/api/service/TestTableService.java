package com.tayobus.sugamaga.api.service;


import com.tayobus.sugamaga.api.request.CustomRequest;
import com.tayobus.sugamaga.api.request.TestTableRequest;
import com.tayobus.sugamaga.db.entity.TestTable;
import com.tayobus.sugamaga.db.entity.User;
import com.tayobus.sugamaga.db.entity.UserCustom;
import com.tayobus.sugamaga.db.repository.CustomRepository;
import com.tayobus.sugamaga.db.repository.SignRepository;
import com.tayobus.sugamaga.db.repository.TestTableRepository;
import lombok.RequiredArgsConstructor;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;

@Service
@RequiredArgsConstructor
public class TestTableService {
    private final Logger logger = LoggerFactory.getLogger(TestTableService.class);
    private final TestTableRepository testTableRepository;

    private final SignRepository signRepository;

    private final CustomRepository customRepository;

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

    public void testPut(CustomRequest request) {
        Optional<User> user = signRepository.findByUid(request.getUid());
        Optional<UserCustom> userCustom = customRepository.findByUserIdx(user.get().getUserIdx());

        logger.info(userCustom.toString());

        userCustom.ifPresent(selectUserCustom -> {
            selectUserCustom.setHair(request.getHair());
            selectUserCustom.setCap(request.getCap());
            customRepository.save(selectUserCustom);
        });
    }

}
