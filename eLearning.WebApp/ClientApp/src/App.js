import React, { Component } from 'react';
import { Route, Switch } from 'react-router';
import { Layout } from './components/Layout';
import { GraphLayout } from './components/GraphLayout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import AuthorizeRoute from './components/api-authorization/AuthorizeRoute';
import ApiAuthorizationRoutes from './components/api-authorization/ApiAuthorizationRoutes';
import { ApplicationPaths } from './components/api-authorization/ApiAuthorizationConstants';

import './custom.css'
import './graph.css'

export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <Switch>
        <Route exact path='/'>
          <GraphLayout>
            <Route exact path='/' component={Home} />
          </GraphLayout>
        </Route>
        <Route>
          <Layout>
            <Switch>
              <Route path='/counter' component={Counter} />
              <AuthorizeRoute path='/fetch-data' component={FetchData} />
              <Route path={ApplicationPaths.ApiAuthorizationPrefix} component={ApiAuthorizationRoutes} />
            </Switch>
          </Layout>
        </Route>
      </Switch>
    );
  }
}
