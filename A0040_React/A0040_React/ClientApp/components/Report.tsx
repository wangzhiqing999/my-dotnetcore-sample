import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';


interface ReportData {
    reportID: number;
    reportName: string;
    reportFileName: string;
}

interface ReportDataState {
    reports: ReportData[];
    loading: boolean;
}

export class ReportList extends React.Component<RouteComponentProps<{}>, ReportDataState> {

    constructor() {
        super();
        this.state = { reports: [], loading: true };

        fetch('api/Reports')
            .then(response => response.json() as Promise<ReportData[]>)
            .then(data => {
                this.setState({ reports: data, loading: false });
            });
    }


    public render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : ReportList.renderForecastsTable(this.state.reports);

        return <div>
            <h1>报表数据</h1>
            <p>下列数据， 通过 Web Api 获取</p>
            {contents}
        </div>;
    }

    private static renderForecastsTable(reports: ReportData[]) {
        return <table className='table'>
            <thead>
                <tr>
                    <th>报表ID</th>
                    <th>报表名称</th>
                    <th>报表文件名</th>
                </tr>
            </thead>
            <tbody>
                {reports.map(report =>
                    <tr key={report.reportID}>
                        <td>{report.reportID}</td>
                        <td>{report.reportName}</td>
                        <td>{report.reportFileName}</td>
                    </tr>
                )}
            </tbody>
        </table>;
    }
}