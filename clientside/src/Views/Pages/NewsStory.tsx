import React, { useState } from "react";
import { useRouteMatch, useHistory, useLocation, Redirect } from "react-router";
import Navigation from "../Components/Navigation/Navigation";
import ReactMarkdown from "react-markdown";
import { ArrowBack, Today } from "@material-ui/icons";
import Page from "Views/Components/Layout/Page";
import { BackButton } from "Views/Components/MaterialComponents/MaterialStyles";
import ComponentSpinner from "Views/Components/Spinner/ComponentSpinner";
import { gql } from "apollo-boost";
import { store } from "Models/Store";
import { useEffect } from "react";
import { NewsArticleEntity } from "Models/Entities";
import moment from "moment";

interface IMatchParams {
	id?: string;
}

export default function NewsStory() {
	const match = useRouteMatch<IMatchParams>();
	const history: any = useHistory();
	const location: any = useLocation();
	const [inProp, setInProp] = useState(false);

	const [article, setArticle] = useState<NewsArticleEntity>(new NewsArticleEntity());
	const [otherArticles, setOtherArticles] = useState<NewsArticleEntity[] | undefined>(undefined);

	// if (!location.state) {
	// 	return <Redirect to={"/404"} />;
	// }

	function handleClick() {
		setInProp(!!!inProp);
	}

	useEffect(() => {
		store.apolloClient.query({ query: queryNewsArticle(match.params.id!) }).then((d) => {
			setArticle(d.data.newsArticleEntity);
		});
		store.apolloClient.query({ query: queryAdditionalArticles(match.params.id!) }).then((d) => {
			setOtherArticles(d.data.newsArticleEntitys);
		});
	}, [match.params]);

	return (
		<Page title="News Story">
			<div className="story-page">
				<div className="story-title" onClick={handleClick}>
					<div className="title">{article.headline}</div>
					<div className="underline" />
				</div>
				<div className="story-container">
					<ReactMarkdown className="story-content" escapeHtml={false}>
						{article.content}
					</ReactMarkdown>
					<div className="additional-articles">
						<div className="additional-header">
							<span>Other</span> Articles
						</div>
						<div className="additonal-articles-container">
							{otherArticles && renderAdditionalArticles(otherArticles)}
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

function queryNewsArticle(id: string) {
	return gql`
	query News{
		newsArticleEntity(id:"${id}"){
		  id
		  headline
		  description
		  content
		}
	  }
	`;
}

function queryAdditionalArticles(id: string) {
	return gql`
		query News {
			newsArticleEntitys(
				where: { path: "id", comparison: notEqual, value: "${id}" }
				take: 6
			) {
				id
				headline
				description
				content
				featureImageId
				created
			}
		}
	`;
}

function renderAdditionalArticles(articles: NewsArticleEntity[]) {
	return articles.map((article: NewsArticleEntity, index: number) => {
		return <Article key={index} article={article} />;
	});
}

export function Article(props: { article: NewsArticleEntity }) {
	const { article } = props;
	// const { open, visible } = getNavigationState(location);
	return (
		<div className="article" onClick={() => store.routerHistory.push(`/news/${article.id}`)}>
			<div className="article-image">
				<img src={`/api/files/${article.featureImageId}`} />
			</div>
			<div className="article-content">
				<div className="article-information">
					<div className="article-title">{article.headline}</div>
					<div className="article-description">{article.description}</div>
				</div>

				<div className="article-date">
					<div className="calendar-icon">
						<Today />
					</div>
					<div className="publish-date">{moment(article.created).format("h:mma Do MMMM YYYY")}</div>
				</div>
			</div>
		</div>
	);
}
