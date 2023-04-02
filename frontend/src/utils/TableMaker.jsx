import React from "react";

import styled from 'styled-components';

const TableMaker = ({ headers, rows }) => {

    return (
        <table>
            <thead>
                <tr>
                    {headers.map(header => (
                        <ThStyle key={header}>{header}</ThStyle>
                    ))}
                </tr>
            </thead>
            <tbody>
                {rows.map(cols => (
                    <tr key={cols.id}>
                        {cols.columns.map(col => (
                            (col.key === 0 ?
                                <TdHeadStyle key={col.key}>{col.render}</TdHeadStyle>
                                : <TdDetailStyle key={col.key}>{col.render}</TdDetailStyle>)
                        ))}
                    </tr>
                ))}
            </tbody>
        </table>
    );
};

export default TableMaker;

const ThStyle = styled.th`
    font-size: 34px;
    font-family: gyeonggi_title_bold;
    text-align: center;
`

const TdHeadStyle = styled.td`
    font-size: 28px;
    font-family: gyeonggi_title_bold;
    text-align: center;
`

const TdDetailStyle = styled.td`
    font-size: 22px;
    font-family: gyeonggi_title_medium;
    text-align: center;
`