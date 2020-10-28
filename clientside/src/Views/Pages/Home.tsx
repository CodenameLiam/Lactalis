import React, { useEffect, useState } from "react";
import axios from "axios";
import Navigation from "../Components/Navigation/Navigation";
// import { Select, MenuItem, FormControl, InputLabel } from '@material-ui/core';
// import { HomeSelect } from './../Styles/MaterialStyles';
import Select from "react-select";
import MilkInfoGraph from "../Components/Charts/MilkInfoGraph";
import moment from "moment";
import { group, info } from "console";
import Page from "Views/Components/Layout/Page";
import { gql } from "apollo-boost";
import { store } from "Models/Store";
import { FarmEntity } from "Models/Entities";
import { getFetchAllQuery } from "Util/EntityUtils";
import { CollectionsOutlined } from "@material-ui/icons";
import Autocomplete from "@material-ui/lab/Autocomplete/Autocomplete";
import {
	AutoCompletePaper,
	InputTextField,
	LoginTextField,
} from "Views/Components/MaterialComponents/MaterialStyles";
import Spinner from "Views/Components/Spinner/Spinner";
import PageSpinner from "Views/Components/Spinner/PageSpinner";
import Charts, { IFarm } from "Views/Components/Charts/Charts";

export default function Home() {
	const farms = useFarmers();
	const [farm, setFarm] = useState<IFarm | null>(null);

	useEffect(() => {
		if (farms) {
			if (farms.length > 0) {
				setFarm(farms[0]);
			}
		}
	}, [farms]);

	if (!farms) {
		return <PageSpinner />;
	}

	return (
		<Page>
			<div className="home">
				<div className="select-farm">
					<div className="select-title">Select Farm:</div>

					<Autocomplete
						className="select-dropdown"
						options={farms}
						getOptionLabel={(option) => option.name}
						onChange={(event: any, newValue: any | null) => {
							setFarm(newValue);
						}}
						defaultValue={farms[0]}
						PaperComponent={AutoCompletePaper}
						renderInput={(params) => (
							<InputTextField {...params} placeholder="Select Farm" variant="outlined" />
						)}
					/>
				</div>

				<Charts farm={farm!} />
			</div>
		</Page>
	);
}

// // Gets the suppliers information
function useFarmers() {
	const [farms, setFarms] = useState<any>(undefined);
	const isAdmin = store.userGroups.find((group: any) => group.name == "Admin") != undefined;
	const query = isAdmin ? queryAdminFarms() : queryFarms();

	useEffect(() => {
		store.apolloClient.query({ query: query }).then((d: any) => {
			const farmArray = isAdmin ? d.data.farmEntitys : d.data.farmerEntity.farmss.farms;
			setFarms(farmArray);
		});
	}, []);

	return farms;
}

function queryFarms() {
	return gql`query Farms{
		farmerEntity(id: "${store.userId}"){
		  farmss{
			farms{
			  id
			  name
			  state
			}
		  }
		}
	  }`;
}

function queryAdminFarms() {
	return gql`
		query AdminFarms {
			farmEntitys {
				id
				name
				state
			}
		}
	`;
}

// // Gets the milk data for a given supplier/date range
// function useGraphs(supplier: number | undefined, dateRange: Date) {
// 	const [volume, setVolume] = useState<any[]>([]);
// 	const [temperature, setTemperature] = useState<any[]>([]);
// 	const [milkInfo, setMilkInfo] = useState<any[]>([]);

// 	useEffect(() => {
// 		if (supplier && dateRange) {
// 			axios.get(`/api/suppliers/volume/${supplier}?after=${dateRange.toJSON()}`).then((d: any) => {
// 				setVolume(
// 					d.data.deliveries.map((entry: any) => {
// 						return { x: entry.dateTime, y: entry.volume };
// 					})
// 				);
// 			});

// 			axios
// 				.get(`/api/suppliers/temperature/${supplier}?after=${dateRange.toJSON()}`)
// 				.then((d: any) => {
// 					setTemperature(
// 						d.data.deliveries.map((entry: any) => {
// 							return { x: entry.dateTime, y: entry.temperature };
// 						})
// 					);
// 				});

// 			axios
// 				.get(`/api/suppliers/milkinfo/${supplier}?after=${dateRange.toJSON()}`)
// 				.then((d: any) => {
// 					setMilkInfo(
// 						d.data.deliveries.map((entry: any) => {
// 							return {
// 								fat: { x: entry.dateTime, y: entry.fat },
// 								protein: { x: entry.dateTime, y: entry.protein },
// 								lactose: { x: entry.dateTime, y: entry.lactose },
// 								snf: { x: entry.dateTime, y: entry.snf },
// 							};
// 						})
// 					);
// 				});
// 		}
// 	}, [supplier, dateRange]);

// 	return { volume, temperature, milkInfo };
// }

// // Renders the graph
// function Graph(props: { currentSupplier: number | undefined; currentDateRange: Date }) {
// 	const { volume, temperature, milkInfo } = useGraphs(
// 		props.currentSupplier,
// 		props.currentDateRange
// 	);

// 	if (volume && temperature && milkInfo) {
// 		return (
// 			<>
// 				<MilkInfoGraph
// 					datasets={[
// 						{
// 							label: "Volume (L)",
// 							borderColor: "rgba(113, 210, 245, 1)",
// 							backgroundColor: "rgba(113, 210, 245, 0.5)",
// 							data: volume,
// 						},
// 					]}
// 				/>
// 				<MilkInfoGraph
// 					datasets={[
// 						{
// 							label: "Temperature (C)",
// 							borderColor: "rgba(252, 230, 176, 1)",
// 							backgroundColor: "rgba(252, 230, 176, 0.5)",
// 							data: temperature,
// 						},
// 					]}
// 				/>
// 				<MilkInfoGraph
// 					datasets={[
// 						{
// 							label: "Fat (%)",
// 							borderColor: "rgba(198, 206, 234, 1)",
// 							backgroundColor: "rgba(198, 206, 234, 0.5)",
// 							data: milkInfo.map((info: any) => {
// 								return info.fat;
// 							}),
// 						},
// 						{
// 							label: "Protein (%)",
// 							borderColor: "rgba(255, 183, 178, 1)",
// 							backgroundColor: "rgba(255, 183, 178, 0.5)",
// 							data: milkInfo.map((info: any) => {
// 								return info.protein;
// 							}),
// 						},
// 						{
// 							label: "Lactose (%)",
// 							borderColor: "rgba(226, 240, 203, 1)",
// 							backgroundColor: "rgba(226, 240, 203, 0.5)",
// 							data: milkInfo.map((info: any) => {
// 								return info.lactose;
// 							}),
// 						},
// 					]}
// 				/>
// 				<MilkInfoGraph
// 					datasets={[
// 						{
// 							label: "SNF (%)",
// 							borderColor: "rgba(146, 243, 230, 1)",
// 							backgroundColor: "rgba(146, 243, 230, 0.5)",
// 							data: milkInfo.map((info: any) => {
// 								return info.snf;
// 							}),
// 						},
// 					]}
// 				/>
// 			</>
// 		);
// 	}

// 	return <></>;
// }

// // The home page
// export default function Home() {
// 	// Options for the selection box
// 	const supplierOptions = useSuppliers();
// 	const [currentSupplier, setCurrentSupplier] = useState<number | undefined>(undefined);

// 	const dateOptions = [
// 		{ value: 1, label: "1 month" },
// 		{ value: 3, label: "3 months" },
// 		{ value: 6, label: "6 months" },
// 		{ value: 12, label: "1 year" },
// 		{ value: 24, label: "2 years" },
// 	];
// 	const [currentDateRange, setCurrentDateRange] = useState<Date>(new Date());

// 	return (
// 		<Page title="Home">
// 			<div className="home-page">
// 				<Select
// 					className="supplier-select"
// 					// styles={selectStyles}
// 					isSearchable={true}
// 					options={supplierOptions}
// 					onChange={(option: any) => setCurrentSupplier(option.value)}
// 					placeholder="Choose Supplier"
// 				/>

// 				<Select
// 					className="date-select"
// 					options={dateOptions}
// 					onChange={(option: any) =>
// 						setCurrentDateRange(moment().subtract(option.value, "M").toDate())
// 					}
// 					placeholder="Choose Date Range"
// 				/>
// 				<div className="milk-info-graphs">
// 					<Graph currentSupplier={currentSupplier} currentDateRange={currentDateRange} />
// 				</div>
// 			</div>
// 		</Page>
// 	);
// }

// const selectStyles = {
// 	control: (provided, state) => ({
// 		...provided,
// 		width: 300,
// 		border: state.isHovered ? 'none' : 'none',
// 		cursor: state.isHovered ? 'pointer' : 'default',
// 	}),
// };
