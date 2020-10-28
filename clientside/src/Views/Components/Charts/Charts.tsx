import { IconButton } from "@material-ui/core";
import { List } from "@material-ui/icons";
import { gql } from "apollo-boost";
import { IMilkTestEntityAttributes } from "Models/Entities/MilkTestEntity";
import { store } from "Models/Store";
import moment from "moment";
import { Moment } from "moment";
import React, { useEffect, useState } from "react";
import { Bar, Line } from "react-chartjs-2";
import PageSpinner from "../Spinner/PageSpinner";
import Popup from "reactjs-popup";

export interface IFarm {
	id: string;
	name: string;
	state: string;
}

interface ICharts {
	farm: IFarm;
}

export default function Charts(props: ICharts) {
	const [pickups, setPickups] = useState<any>(undefined);

	useEffect(() => {
		if (props.farm) {
			store.apolloClient.query({ query: queryPickups(props.farm.id) }).then((d) => {
				setPickups(d.data.farmEntity.pickupss);
			});
		}
	}, [props.farm]);

	if (!props.farm || !pickups) {
		return <PageSpinner />;
	}

	return (
		<div className="charts">
			<MilkGraph
				records={pickups}
				type="volume"
				title="Volume (L)"
				borderColor="rgba(113, 210, 245, 1)"
				backgroundColor="rgba(113, 210, 245, 0.7)"
			/>
			<MilkGraph
				records={pickups}
				type="protein"
				title="Protein (%)"
				borderColor="rgba(146, 243, 230, 1)"
				backgroundColor="rgba(146, 243, 230, 0.7)"
			/>
			<MilkGraph
				records={pickups}
				type="milkFat"
				title="Fat (%)"
				borderColor="rgba(255,149,174,1)"
				backgroundColor="rgba(255,149,174,0.7)"
			/>
			<MilkGraph
				records={pickups}
				type="temperature"
				title="Temperature (C)"
				borderColor="rgba(67,136,244,1)"
				backgroundColor="rgba(67,136,244,0.7)"
			/>
			<StateAverages state={props.farm.state} records={pickups} />
		</div>
	);
}

interface IStateAverages {
	state: string;
	records: IMilkTestEntityAttributes[];
}

function StateAverages(props: IStateAverages) {
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
		// stateData.push(

		// );

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

interface IMilkGraph {
	records: IMilkTestEntityAttributes[];
	type: string;
	title: string;
	borderColor: string;
	backgroundColor: string;
}

function MilkGraph(props: IMilkGraph) {
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

function queryPickups(farmId: string) {
	return gql`
		query Pickups {
			farmEntity(id: "${farmId}") {
				id
				pickupss (orderBy: {path: "time", descending: true }) {
					time
					milkFat
					temperature
					protein
					volume
				}
			}
		}
	`;
}
