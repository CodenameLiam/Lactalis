import { IconButton } from "@material-ui/core";
import { List } from "@material-ui/icons";
import { IMilkTestEntityAttributes } from "Models/Entities/MilkTestEntity";
import moment from "moment";
import React, { useState } from "react";
import { Line } from "react-chartjs-2";
import Popup from "reactjs-popup";

interface IMilkGraph {
	records: IMilkTestEntityAttributes[];
	type: string;
	title: string;
	borderColor: string;
	backgroundColor: string;
}

export default function MilkGraph(props: IMilkGraph) {
	const [period, setPeriod] = useState("month");
	const periodOptions = {
		week: { label: "Week", stepLength: 7, stepUnit: "day" },
		month: { label: "Month", stepLength: 31, stepUnit: "week" },
		year: { label: "Year", stepLength: 11, stepUnit: "month" },
	};
	const selectedOptions = periodOptions[period];

	let labels: any = [];
	let volumeData: any[] = [];

	if (props.records.length > 0) {
	}

	if (period === "year") {
		let monthlyTotals = {};
		let monthlyAverages = {};
		props.records.forEach((record, i) => {
			if (moment(props.records[i].time).isBetween(moment().subtract(1, "year"), moment())) {
				if (monthlyTotals[moment(props.records[i].time).format("MMM")] == undefined) {
					monthlyTotals[moment(props.records[i].time).format("MMM")] = 0;
				}
				monthlyTotals[moment(props.records[i].time).format("MMM")] += props.records[i][props.type];
			}
		});

		for (let i = selectedOptions.stepLength; i >= 0; i--) {
			const month = moment().subtract(i, "month");
			const monthFormat = month.format("MMM");
			monthlyAverages[monthFormat] = (monthlyTotals[monthFormat] / month.daysInMonth()).toFixed(2);
		}
		Object.entries(monthlyAverages).map(([month, volume]) => {
			labels.push(month);
			volumeData.push(volume);
		});
	} else {
		for (let i = 0; i < selectedOptions.stepLength; i++) {
			if (props.records.length > 0) {
				labels.push(moment(props.records[i].time).format("MMM D"));
				volumeData.push(props.records[i][props.type]);
			}
		}
	}

	const data = {
		labels: labels,
		datasets: [
			{
				label: props.title,
				data: volumeData,
				borderWidth: 1,
				borderColor: props.borderColor,
				backgroundColor: props.backgroundColor,
			},
		],
	};

	const options = {
		title: {
			display: false,
		},
		legend: {
			display: false,
		},
		animation: {
			duration: 2000,
		},
		layout: {
			padding: 10,
		},
		maintainAspectRatio: false,
		scales: {
			xAxes: [
				{
					gridLines: {
						display: false,
					},
				},
			],
			yAxes: [
				{
					ticks: {
						beginAtZero: true,
						// suggestedMax: Math.max(...volumeData),
					},
				},
			],
		},
	};

	return (
		<div className="info-card">
			<div className="info-head">
				<div className="info-title">{props.title}</div>
				<div className="info-settings">
					<Popup
						className="period-select"
						trigger={
							<IconButton>
								<List />
							</IconButton>
						}
						position="left top">
						{(close: () => void) => (
							<div className="period-options">
								{Object.entries(periodOptions).map(([periodOption, options]) => (
									<div
										className={`option ${period == periodOption && "option-active"}`}
										onClick={() => {
											setPeriod(periodOption);
											close();
										}}>
										{options.label}
									</div>
								))}
							</div>
						)}
					</Popup>
				</div>
			</div>

			<div className="graph">
				<Line data={data} options={options} />
			</div>
		</div>
	);
}
