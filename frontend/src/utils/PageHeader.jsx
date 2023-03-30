import React from "react";

import HorizonLine from "./HorizonLine";

const PageHeader = ({ title, horizonTitle }) => {

    return (
        <div>
            <h1>{title}</h1>
            <HorizonLine text = { horizonTitle } />
        </div>
    );
};

export default PageHeader;