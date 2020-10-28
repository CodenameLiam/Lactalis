import React from "react";
// import { Line } from 'react-chartjs-2';

interface dataset {
	data: any;
	label: string;
	borderColor: string;
	backgroundColor: string;
}

export default function MilkInfo(props: { datasets: dataset[] }) {
	return (
		<div className="milk-info-chart-container">
			<div className="milk-info-chart">
				{/* <div className="info-title-container">
			{props.datasets.map((set: dataset, index: number) => {
				return (
					<div key={index} className="info-title">
						{set.label}
					</div>
				);
			})}
		</div> */}
				{/* <Line
					data={{
						datasets: props.datasets.map((set: dataset) => {
							return {
								data: set.data,
								label: set.label,
								fill: true,
								borderColor: set.borderColor,
								backgroundColor: set.backgroundColor,
							};
						}),
					}}
					options={{
						title: {
							display: false,
						},
						animation: {
							duration: 2000,
						},
						layout: {
							padding: 10,
						},
						maintainAspectRatio: false,
						// legend: {
						// 	display: false,
						// },
						scales: {
							xAxes: [
								{
									type: "time",
								},
							],
							yAxes: [
								{
									ticks: {
										beginAtZero: true,
									},
								},
							],
						},
					}}
				/> */}
			</div>
		</div>
	);
}
