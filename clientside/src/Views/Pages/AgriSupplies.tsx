import { ListAlt, PictureAsPdf } from "@material-ui/icons";
import { gql } from "apollo-boost";
import Axios from "axios";
import { AgriSupplyDocumentEntity } from "Models/Entities";
import { store } from "Models/Store";
import React, { useEffect, useState } from "react";
import DocumentPage from "Views/Components/Layout/DocumentPage";
import Page from "Views/Components/Layout/Page";
import ComponentSpinner from "Views/Components/Spinner/ComponentSpinner";

export default function AgriSupplies() {
	const [documents, setDocuments] = useState<any[] | undefined>(undefined);

	useEffect(() => {
		store.apolloClient.query({ query: queryAgriSupplies() }).then((d) => {
			let _documents: any = [];
			let promises: any = [];

			for (let document of d.data.agriSupplyDocumentEntitys) {
				promises.push(
					Axios.get(`/api/files/metadata/${document.fileId}`).then((d) => {
						_documents.push({ ...document, contentType: d.data.contentType });
					})
				);
			}
			Promise.all(promises).then(() => setDocuments(_documents));
		});
	}, []);

	return (
		<Page title="Agri Supplies">
			<DocumentPage>{renderDocuments()}</DocumentPage>
		</Page>
	);

	function renderDocuments() {
		if (documents) {
			console.log(documents);
			return documents.map((document) => {
				return (
					<div
						className="info-card"
						key={document.id}
						onClick={() => window.open(`/api/files/${document.fileId}`)}>
						<div className="info-head info-head-no-margin">
							<div className="info-title">{document.name}</div>
						</div>
						<div className="document-icon">{icons[document.contentType]}</div>
					</div>
				);
			});
		}
		return <ComponentSpinner />;
	}
}

const icons = {
	"application/pdf": <PictureAsPdf />,
	"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet": <ListAlt />,
};

function queryAgriSupplies() {
	return gql`
		query AgriSupplies {
			agriSupplyDocumentEntitys {
				id
				name
				fileId
			}
		}
	`;
}
