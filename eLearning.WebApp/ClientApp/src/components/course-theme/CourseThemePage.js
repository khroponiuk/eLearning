import React, { useState, useEffect } from 'react';
import api from '../../utils/api'
import Spinner from '../Spinner';
import { Card, Nav, Tabs, Tab } from 'react-bootstrap'
import { Lab } from './Lab';
import { Lecture } from './Lecture';
import authService from '../api-authorization/AuthorizeService'
import { Settings } from './Settings';
import { Quiz } from './Quiz';
import { QuizStats } from './QuizStats';


export function CourseThemePage(props) {
  const [loading, setLoading] = useState(true);
  const [themeState, setThemeState] = useState(null);

  const testFile = "https://localhost:5001/files/test.pdf"

  const [isAdmin, setIsAdmin] = useState(false);

  useEffect(() => {
    async function fetchData() {
      const themeData = await api.get('api/theme?id=' + props.match.params.id);
      const userData = await authService.isInRole('Admin');

      setThemeState(themeData);
      setIsAdmin(userData);
      setLoading(false);
    }

    fetchData();
  }, []);

  function configurationUpdateHandler(data) {
    setThemeState(data);
  }

  return (
    <>
      {
        loading ?
          <Spinner /> :
          <Tab.Container id="left-tabs-example" defaultActiveKey="lecture">
            <Card>
              <Card.Header>
                <Nav variant="tabs" defaultActiveKey="lecture">
                  <Nav.Item>
                    <Nav.Link eventKey="lecture">Lecture</Nav.Link>
                  </Nav.Item>
                  <Nav.Item>
                    <Nav.Link eventKey="lab">Lab</Nav.Link>
                  </Nav.Item>
                  <Nav.Item>
                    <Nav.Link eventKey="quiz">Quiz</Nav.Link>
                  </Nav.Item>
                  {
                    isAdmin ?
                      <>
                        <Nav.Item>
                          <Nav.Link eventKey="stats">Statistics</Nav.Link>
                        </Nav.Item>
                        <Nav.Item>
                          <Nav.Link eventKey="settings">Settings</Nav.Link>
                        </Nav.Item>
                      </>
                      : null
                  }
                </Nav>
              </Card.Header>
              <Card.Body>
                <Tab.Content>
                  <Tab.Pane eventKey="lecture">
                    <Lecture filePath={themeState.lecture.filePath} />
                  </Tab.Pane>
                  <Tab.Pane eventKey="lab">
                    <Lab filePath={themeState.lab.filePath} />
                  </Tab.Pane>
                  <Tab.Pane eventKey="quiz">
                    <Quiz id={themeState.quiz.externalQuizId} />
                  </Tab.Pane>
                  {
                    isAdmin ?
                      <>
                        <Tab.Pane eventKey="stats">
                          <QuizStats id={themeState.quiz.externalQuizId} />
                        </Tab.Pane>
                        <Tab.Pane eventKey="settings">
                          <Settings
                            themeId={themeState.id}
                            isLectureEnabled={themeState.isLectureEnabled}
                            isLabEnabled={themeState.isLabEnabled}
                            isQuizEnabled={themeState.isQuizEnabled}
                            lectureFilePath={themeState.lecture.filePath}
                            labFilePath={themeState.lab.filePath}
                            quizId={themeState.quiz.externalQuizId}
                            setThemeState={configurationUpdateHandler} />
                        </Tab.Pane>
                      </>
                      : null
                  }

                </Tab.Content>
              </Card.Body>
            </Card>
          </Tab.Container>
      }
    </>
  );
}