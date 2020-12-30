import React, { Component } from 'react';
import { Route, Switch } from 'react-router';
import { Layout } from './components/layouts/Layout';
import { GraphLayout } from './components/layouts/GraphLayout';
import Home from './components/Home';
import CoursePage from './components/CoursePage';
import { CourseThemePage } from './components/course-theme/CourseThemePage';
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
            <AuthorizeRoute exact path='/' component={Home} />
          </GraphLayout>
        </Route>
        <Route path='/course'>
          <Switch>
            <GraphLayout>
              <AuthorizeRoute path='/course/:id' component={CoursePage} />
            </GraphLayout>
          </Switch>
        </Route>
        <Route>
          <Layout>
            <Switch>
              <AuthorizeRoute path='/theme/:id' component={CourseThemePage} />
              <Route path={ApplicationPaths.ApiAuthorizationPrefix} component={ApiAuthorizationRoutes} />
            </Switch>
          </Layout>
        </Route>
      </Switch>
    );
  }
}
