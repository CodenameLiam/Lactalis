import React, { useState } from "react";
import { useRouteMatch, useHistory, useLocation, Redirect } from "react-router";
import Navigation from "../Components/Navigation/Navigation";
import ReactMarkdown from "react-markdown";
import { IArticle, Article } from "./News";
import { BackButton } from "../../Styles/MaterialStyles";
import { ArrowBack } from "@material-ui/icons";
import Page from "Views/Components/Layout/Page";

export default function NewsStory() {
	const match: any = useRouteMatch();
	const history: any = useHistory();
	const location: any = useLocation();
	const [inProp, setInProp] = useState(false);

	if (!location.state) {
		return <Redirect to={"/404"} />;
	}

	function handleClick() {
		setInProp(!!!inProp);
		console.log(inProp);
	}

	return (
		<Page title="News Story">
			<div className="story-page">
				<div onClick={() => handleClick()} className="story-title">
					<div className="title">{location.state.title}</div>
					<div className="underline" />
				</div>
				<div className="story-container">
					<ReactMarkdown className="story-content" escapeHtml={false}>
						{location.state.content}
					</ReactMarkdown>
					<div className="additional-articles">
						<div className="additional-header">
							<span>Other</span> Articles
						</div>
						<div className="additonal-articles-container">
							{renderAdditionalArticles(history, location, location.state.articles)}
						</div>
						<div className="return-button" onClick={() => history.push("/news")}>
							<BackButton>
								<ArrowBack />
								<span>Back To News</span>
							</BackButton>
						</div>
					</div>
				</div>
			</div>
		</Page>
	);
}

function renderAdditionalArticles(history: any, location: any, articles: IArticle[]) {
	return articles.map((article: IArticle, index: number) => {
		return <Article key={index} history={history} location={location} article={article} />;
	});
}
