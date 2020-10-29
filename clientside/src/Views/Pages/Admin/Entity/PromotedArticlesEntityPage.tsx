import * as React from "react";
import { observer } from "mobx-react";
import { RouteComponentProps } from "react-router";
import * as Models from "Models/Entities";
import EntityCRUD from "Views/Components/CRUD/EntityCRUD";
import { PageWrapper } from "Views/Components/PageWrapper/PageWrapper";
import PageLinks from "../PageLinks";
import SecuredPage from "Views/Components/Security/SecuredPage";

@observer
export default class PromotedArticlesEntityPage extends React.Component<RouteComponentProps> {
	public render() {
		let contents = null;

		contents = (
			<PageWrapper {...this.props}>
				<EntityCRUD modelType={Models.PromotedArticlesEntity} {...this.props} />
			</PageWrapper>
		);

		return <SecuredPage groups={["Admin", "Farmer"]}>{contents}</SecuredPage>;
	}
}
