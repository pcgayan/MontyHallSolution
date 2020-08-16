import { Action, Reducer } from 'redux';
import { AppThunkAction } from './';
import { stat } from 'fs';
import * as PlayerStore from '../store/Player';

export interface MontyHallSimulatorState {
    isLoading: boolean;
    gameCount: number;
    switchDoor: boolean;
    games: GameState[];
    error: string;
    accessToekn: string
}

export interface GameState {
    isLoading: boolean;
    id: number;
    stage: string;
    state: boolean;
    message: string;
}

interface RequestMontyHallSimulatorAction {
    type: 'REQUEST_MONTY_HALL_SIMULATOR_ACTION';
    gameCount: number;
    switchDoor: boolean;
    accessToekn: string;
}

interface ReceivetMontyHallSimulatorAction {
    type: 'RECEIVE_MONTY_HALL_SIMULATOR_ACTION';
    gameCount: number;
    switchDoor: boolean;
    game: GameState;
}

interface ReceivetErrorAction {
    type: 'RECEIVE_ERROR_ACTION';
    error: string;
}

type KnownAction = RequestMontyHallSimulatorAction | ReceivetMontyHallSimulatorAction | ReceivetErrorAction;

export const actionCreators = {
    requestMontyHallSimulatorAction: (gameCount: number, switchDoor: boolean, accessToekn: string): AppThunkAction<KnownAction> => (dispatch, getState) => {

        const appState = getState();
        if (appState && appState.montyHallSimulatorState && !appState.montyHallSimulatorState.isLoading && appState.montyHallSimulatorState.error.length == 0 && appState.montyHallSimulatorState.games.length < gameCount) {
            const url = 'api/montyhall/GameStart';
            fetch(url,
                {
                    method: 'get',
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': 'Bearer ' + JSON.parse(accessToekn)
                    }
                })
                .then(async response => {
                    if (!response.ok) {
                        //dispatch({ type: 'RECEIVE_ERROR_ACTION', error: error });
                        return Promise.reject(response.status);
                    } else {
                        var data = await response.json();
                        console.log(data);
                        //const outGames = new [isLoading : false]
                        dispatch({ type: 'RECEIVE_MONTY_HALL_SIMULATOR_ACTION', gameCount: gameCount, switchDoor: switchDoor, game: data });
                    }
                })
                .catch(error => {
                    dispatch({ type: 'RECEIVE_ERROR_ACTION', error: error });
                });

            dispatch({ type: 'REQUEST_MONTY_HALL_SIMULATOR_ACTION', gameCount: gameCount, switchDoor: switchDoor, accessToekn: accessToekn });
        }
    }
}

const unloadedState: MontyHallSimulatorState = { isLoading: false, gameCount: 0, switchDoor: false, games: [], error: '', accessToekn: '' };

export const reducer: Reducer<MontyHallSimulatorState> = (state: MontyHallSimulatorState | undefined, incomingAction: Action): MontyHallSimulatorState => {
     if (state === undefined) {
        return unloadedState;
    }

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_MONTY_HALL_SIMULATOR_ACTION':
            console.log(action);
             return {
                isLoading: true,
                gameCount: state.gameCount,
                switchDoor: state.switchDoor,
                games: state.games,
                 error: '',
                 accessToekn: state.accessToekn
            };
        case 'RECEIVE_MONTY_HALL_SIMULATOR_ACTION':
            console.log(action);
            return {
                isLoading: false,
                gameCount: state.gameCount,
                switchDoor: state.switchDoor,
                games: Object.assign({}, action.game),
                error: '',
                accessToekn: state.accessToekn
            };
        case 'RECEIVE_ERROR_ACTION':
            console.error(action.error);
            return {
                isLoading: false,
                gameCount: 0,
                switchDoor: false,
                games: [],
                error: action.error,
                accessToekn: ''
            };
    }

    return state;
};