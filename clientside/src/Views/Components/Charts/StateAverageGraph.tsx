import { IconButton } from "@material-ui/core";
import { List } from "@material-ui/icons";
import { IMilkTestEntityAttributes } from "Models/Entities/MilkTestEntity";
import moment from "moment";
import React, { useState } from "react";
import { Bar } from "react-chartjs-2";
import Popup from "reactjs-popup";

interface IStateAverages {
	state: string;
	records: IMilkTestEntityAttributes[];
}

export default function StateAverageGraph(props: IStateAverages) {
	const stateData = [
		{ volume: 8569, milkFat: 3, protein: 4.2, temperature: 3.4 },
		{ volume: 9645, milkFat: 3, protein: 3.1, temperature: 3.5 },
		{ volume: 7467, milkFat: 3.1, protein: 4.1, temperature: 3.6 },
		{ volume: 9456, milkFat: 3.2, protein: 4, temperature: 3.5 },
		{ volume: 4213, milkFat: 3.3, protein: 3.6, temperature: 3.6 },
		{ volume: 6789, milkFat: 3.6, protein: 3.2, temperature: 3.5 },
		{ volume: 9876, milkFat: 3.6, protein: 3.1, temperature: 3.7 },
		{ volume: 7689, milkFat: 3.4, protein: 3.5, temperature: 3.5 },
		{ volume: 8768, milkFat: 3.5, protein: 3.6, temperature: 3.2 },
		{ volume: 6234, milkFat: 3.4, protein: 3.4, temperature: 3.3 },
		{ volume: 7787, milkFat: 3.2, protein: 3.5, temperature: 3.1 },
		{ volume: 7865, milkFat: 3.5, protein: 3.6, temperature: 3.2 },
	];

	const [type, setType] = useState("volume");
	const typeOptions = {
		volume: {
			label: "Volume",
			borderColor: "rgba(113, 210, 245, 1)",
			backgroundColor: "rgba(113, 210, 245, 0.7)",
		},
		protein: {
			label: "Protein",
			borderColor: "rgba(146, 243, 230, 1)",
			backgroundColor: "rgba(146, 243, 230, 0.7)",
		},
		milkFat: {
			label: "Fat",
			borderColor: "rgba(255,149,174,1)",
			backgroundColor: "rgba(255,149,174,0.7)",
		},
		temperature: {
			label: "Temperature",
			borderColor: "rgba(67,136,244,1)",
			backgroundColor: "rgba(67,136,244,0.7)",
		},
	};

	let labels: any = [];
	let myData: any[] = [];

	let monthlyTotals = {};
	let monthlyAverages = {};
	props.records.forEach((record, i) => {
		if (moment(props.records[i].time).isBetween(moment().subtract(1, "year"), moment())) {
			if (monthlyTotals[moment(props.records[i].time).format("MMM")] == undefined) {
				monthlyTotals[moment(props.records[i].time).format("MMM")] = 0;
			}
			monthlyTotals[moment(props.records[i].time).format("MMM")] += props.records[i][type];
		}
	});

	for (let i = 11; i >= 0; i--) {
		const month = moment().subtract(i, "month");
		const monthFormat = month.format("MMM");
		labels.push(monthFormat);
		monthlyAverages[monthFormat] = (monthlyTotals[monthFormat] / month.daysInMonth()).toFixed(2);
	}

	Object.values(monthlyAverages).map((data) => {
		myData.push(data);
	});

	const data = {
		labels: labels,
		datasets: [
			{
				label: "State Data",
				data: stateData.map((data) => data[type]),
				borderWidth: 1,
				borderColor: "rgba(164,122,224,1)",
				backgroundColor: "rgba(164,122,224,0.7)",
			},
			{
				label: "My Data",
				data: myData,
				borderWidth: 1,
				borderColor: typeOptions[type].borderColor,
				backgroundColor: typeOptions[type].backgroundColor,
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
		<div className="info-card-wide">
			<div className="info-head">
				<div className="info-title">{`${typeOptions[type].label} Averages vs ${props.state} `}</div>
				<div className="info-label">
					<div className="circle" style={{ background: "rgba(164,122,224,1)" }} />
					<div className="label">State Data</div>
					<div className="circle" style={{ background: typeOptions[type].borderColor }} />
					<div className="label">My Data</div>
				</div>
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
								{Object.entries(typeOptions).map(([typeOption, options]) => (
									<div
										className={`option ${type == typeOption && "option-active"}`}
										onClick={() => {
											setType(typeOption);
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
				<Bar data={data} options={options} />
			</div>
		</div>
	);
}
