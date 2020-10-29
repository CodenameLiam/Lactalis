import React, { useEffect, useState } from "react";
import Page from "Views/Components/Layout/Page";
import { gql } from "apollo-boost";
import { store } from "Models/Store";
import Autocomplete from "@material-ui/lab/Autocomplete/Autocomplete";
import {
	AutoCompletePaper,
	InputTextField,
} from "Views/Components/MaterialComponents/MaterialStyles";
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
			const farmArray = isAdmin
				? d.data.farmEntitys
				: d.data.farmerEntity.farmss.map((farm: any) => farm.farms);
			setFarms(farmArray);
		});
	}, []);

	return farms;
}

function queryFarms() {
	return gql`query Farms{
		farmerEntity(id: "${store.userId}"){
		  id
		  farmss{
			id
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
