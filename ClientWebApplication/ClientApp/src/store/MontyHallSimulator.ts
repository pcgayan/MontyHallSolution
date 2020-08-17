import { Action, Reducer } from 'redux';
import { AppThunkAction } from './';
import { stat } from 'fs';
import * as PlayerStore from '../store/Player';

export interface MontyHallSimulatorState {
    isLoading: boolean;
    numberOfSimulations: number;
    switchDoor: boolean;
    games: GameState[];
    error: string;
    accessToekn: string,
    currentSimulationAtIndex: number;
}

export interface GameState {
    isLoading: boolean;
    id: number;
    stage: number;
    status: string;
    message: string;
    data: string;
}

interface RequestMontyHallSimulatorAction {
    type: 'REQUEST_MONTY_HALL_SIMULATOR_ACTION';
    numberOfSimulations: number;
    switchDoor: boolean;
    accessToekn: string;
    currentSimulationAtIndex: number;
}

interface ReceivetMontyHallSimulatorAction {
    type: 'RECEIVE_MONTY_HALL_SIMULATOR_ACTION';
    game: GameState;
    numberOfSimulations: number;
    currentSimulationAtIndex: number;
}

interface ReceivetErrorAction {
    type: 'RECEIVE_ERROR_ACTION';
    error: string;
}

type KnownAction = RequestMontyHallSimulatorAction | ReceivetMontyHallSimulatorAction | ReceivetErrorAction;

export const actionCreators = {
    requestMontyHallSimulatorAction: (numberOfSimulations: number, switchDoor: boolean, accessToekn: string): AppThunkAction<KnownAction> => (dispatch, getState) => {

        const appState = getState();
        if (appState && appState.montyHallSimulatorState && !appState.montyHallSimulatorState.isLoading &&
            appState.montyHallSimulatorState.error.length == 0 && appState.montyHallSimulatorState.games.length <= numberOfSimulations) {

            var currentSimulationAtIndex = appState.montyHallSimulatorState.currentSimulationAtIndex;  //appState.montyHallSimulatorState.games.length-1;
            const runnigGame = appState.montyHallSimulatorState.games[currentSimulationAtIndex];
            //console.log("runnigGame with index " + currentSimulationAtIndex + " currrent running game " + appState.montyHallSimulatorState.games.length);
            if (runnigGame === undefined) {
                pullGameStatus(accessToekn, dispatch, numberOfSimulations, currentSimulationAtIndex, 'api/montyhall/GameStart');
            } else if (runnigGame.stage === 1) {
                if (runnigGame.status === 'ongoing') {
                    pullGameStatus(accessToekn, dispatch, numberOfSimulations, currentSimulationAtIndex, 'api/montyhall/FirstDoorSelection/3');
                } else {
                    pullGameStatus(accessToekn, dispatch, numberOfSimulations, currentSimulationAtIndex, 'api/montyhall/GameStart');
                }
            } else if (runnigGame.stage === 2) {
                if (runnigGame.status == 'ongoing') {
                    pullGameStatus(accessToekn, dispatch, numberOfSimulations, currentSimulationAtIndex, 'api/montyhall/HostDoorSelection');
                } else {
                    pullGameStatus(accessToekn, dispatch, numberOfSimulations, currentSimulationAtIndex, 'api/montyhall/GameStart');
                }
            } else if (runnigGame.stage === 3) {
                if (runnigGame.status == 'ongoing') {
                    pullGameStatus(accessToekn, dispatch, numberOfSimulations, currentSimulationAtIndex, 'api/montyhall/SwitchDoorSelection/' + ((switchDoor == true) ? '1' : '3'));
                } else {
                    pullGameStatus(accessToekn, dispatch, numberOfSimulations, currentSimulationAtIndex, 'api/montyhall/GameStart');
                }
            } else if (runnigGame.stage === 4) {
                if (runnigGame.status === 'ongoing') {
                    pullGameStatus(accessToekn, dispatch, numberOfSimulations, currentSimulationAtIndex, 'api/montyhall/WinnerStatus');
                } else {
                    //console.log("Game Over for " + currentSimulationAtIndex + " user " + runnigGame.status);

                    currentSimulationAtIndex++;
                    if (currentSimulationAtIndex == numberOfSimulations -1) {
                        return;
                    }
                    pullGameStatus(accessToekn, dispatch, numberOfSimulations, currentSimulationAtIndex, 'api/montyhall/GameStart');
                }
            } 

            dispatch({ type: 'REQUEST_MONTY_HALL_SIMULATOR_ACTION', numberOfSimulations: numberOfSimulations, switchDoor: switchDoor, accessToekn: accessToekn, currentSimulationAtIndex: currentSimulationAtIndex });
        } 
    }
}

const unloadedState: MontyHallSimulatorState = { isLoading: false, numberOfSimulations: 0, switchDoor: false, games: [], error: '', accessToekn: '', currentSimulationAtIndex: 0 };

export const reducer: Reducer<MontyHallSimulatorState> = (state: MontyHallSimulatorState | undefined, incomingAction: Action): MontyHallSimulatorState => {
     if (state === undefined) {
        return unloadedState;
    }

    const action = incomingAction as KnownAction;
    //console.log(action.type);
    switch (action.type) {
        case 'REQUEST_MONTY_HALL_SIMULATOR_ACTION':
             //console.log("state index " + state.currentSimulationAtIndex + " action for index " + action.currentSimulationAtIndex);
             return {
                 isLoading: true,
                 numberOfSimulations: action.numberOfSimulations,
                 switchDoor: action.switchDoor,
                 games: state.games,
                 error: '',
                 accessToekn: action.accessToekn,
                 currentSimulationAtIndex: action.currentSimulationAtIndex
            };
        case 'RECEIVE_MONTY_HALL_SIMULATOR_ACTION':
            //console.log(action);
            //const mergedGames = state.games.concat(action.game);
            //console.log(action.game.stage + " + " + action.game.message);
            const runningGame = state.games[action.currentSimulationAtIndex];
            if (runningGame === undefined) {
                state.games = state.games.concat(action.game);
            } else {
                //state.games = state.games.concat(action.game);

                for (var i = 0; i < state.games.length; i++) {
                    if (state.games[i].id === action.game.id) {
                        //console.log(state.games[i].message);
                        //console.log(action.game.message);
                        if (action.game.stage == 4 && action.game.status === "complete") {
                            action.game.status = action.game.data;
                        }
                         
                        state.games[i] = action.game;
                        break;
                    }
                }
            }

            /*for (var i = 0; i < state.games.length; i++) {
                console.log(state.games[i].id + "," + state.games[i].stage + "," + state.games[i].message + "," + state.games[i].status);
            }*/

            return {
                isLoading: false,
                numberOfSimulations: state.numberOfSimulations,
                switchDoor: state.switchDoor,
                games: state.games,
                error: '',
                accessToekn: state.accessToekn,
                currentSimulationAtIndex: state.currentSimulationAtIndex
            };
        case 'RECEIVE_ERROR_ACTION':
            console.error(action.error);
            return {
                isLoading: false,
                numberOfSimulations: 0,
                switchDoor: false,
                games: [],
                error: action.error,
                accessToekn: '',
                currentSimulationAtIndex: 0
            };
    }

    return state;
};

export function pullGameStatus(accessToekn: string, dispatch: (action: KnownAction) => void, numberOfSimulations: number, currentSimulationAtIndex: number, url: string) {
    fetch(url, {
        method: 'get',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + JSON.parse(accessToekn)
        }
    })
    .then(async (stageOneResponse) => {
        if (!stageOneResponse.ok) {
            return Promise.reject(stageOneResponse.status);
        }
        else {
            var data = await stageOneResponse.json();
            //console.log(data);
            dispatch({ type: 'RECEIVE_MONTY_HALL_SIMULATOR_ACTION', numberOfSimulations: numberOfSimulations, currentSimulationAtIndex: currentSimulationAtIndex, game: data });
        }
    })
    .catch(error => {
        dispatch({ type: 'RECEIVE_ERROR_ACTION', error: error });
    });
}
