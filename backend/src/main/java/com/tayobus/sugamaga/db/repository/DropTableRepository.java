package com.tayobus.sugamaga.db.repository;

import com.tayobus.sugamaga.db.entity.DropTable;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public interface DropTableRepository extends JpaRepository<DropTable, Integer> {
    List<DropTable> findByTableIdx(int tableIdx);
}
