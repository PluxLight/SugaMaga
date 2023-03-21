package com.tayobus.sugamaga.db.repository;

import com.tayobus.sugamaga.db.entity.TestTable;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import java.util.List;
import java.util.Optional;

@Repository
public interface TestTableRepository extends JpaRepository<TestTable, Long> {
    Optional<TestTable> findByStrCol(String strCol);
    Optional<TestTable> findByIntCol(int intCol);
    List<TestTable> findAllByIntCol(int intCol);
}