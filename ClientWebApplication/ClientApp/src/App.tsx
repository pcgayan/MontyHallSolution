import * as React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';
import Counter from './components/Counter';
import FetchData from './components/FetchData';
import FetchPlayerData from './components/FetchPlayerData';
import MontyHallSimulatorData from './components/FetchMontyHallSimulatorData';

import './custom.css'

export default () => (
    <Layout>
        <Route exact path='/' component={FetchPlayerData} />
        <Route path='/player' component={FetchPlayerData} />
        <Route path='/home' component={Home} />
        <Route path='/counter' component={Counter} />
        <Route path='/fetch-data/:startDateIndex?' component={FetchData} />
        <Route path='/monty-hall-simulator' component={MontyHallSimulatorData} />
    </Layout>
);
