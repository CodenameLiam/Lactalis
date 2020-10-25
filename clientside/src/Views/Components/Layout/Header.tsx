import React from "react";
import { AccountCircle, LockOpen, Security } from "@material-ui/icons";
import If from "../If/If";
import { useHistory, useLocation } from "react-router";
import { store } from "Models/Store";

interface IHeader {
	title: string;
}

export default function Header(props: IHeader) {
	// const history = useHistory();
	// const location = useLocation();
	// const navigationMargin = store.navigationOpen ? { marginLeft: "16rem" } : {};

	// const authentication = useSelector((state: any) => state.authentication);
	// const backend = useSelector((state: any) => state.navigation.backendRoute);
	// const dispatch = useDispatch();

	// const hasBackendAccess =
	// 	authentication.user.groups.find((group) => group.hasBackendAccess) !== undefined;

	return (
		<div className="header">
			<div className="left" style={{}}>
				<div className="title">{props.title}</div>
			</div>
			<div className="right">
				<div className="name">
					{/* {authentication.user.firstName} {authentication.user.lastName} */}
				</div>
				<div className="account">
					<AccountCircle />
				</div>
				{/* <If condition={hasBackendAccess}>
					<div
						className="backend"
						onClick={() => {
							dispatch(returnFrontend(location.pathname));
							history.push(backend);
						}}>
						<LockOpen />
					</div>
				</If> */}
			</div>
		</div>
	);
}
