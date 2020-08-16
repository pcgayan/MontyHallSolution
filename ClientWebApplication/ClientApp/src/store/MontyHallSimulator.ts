import { Action, Reducer } from 'redux';
import { AppThunkAction } from './';
import { stat } from 'fs';

export interface PlayerState {
    isLoading: boolean;
    personalId: number;
    accessToken: String;
    expiry: number;
}

interface RequestPlayerAccessTokenAction {
    type: 'REQUEST_PLAYER_ACCESS_TOKEN';
    personalId: number;
}


interface ReceivePlayerAccessTokenAction {
    type: 'RECEIVE_PLAYER_ACCESS_TOKEN';
    personalId: number;
    accessToken: String;
    expiry: number;
}

type KnownAction = RequestPlayerAccessTokenAction | ReceivePlayerAccessTokenAction;

export const actionCreators = {
    requestPlayerAccessToken: (personalId: number): AppThunkAction<KnownAction> => (dispatch, getState) => {

        const appState = getState();
        try {
            if (appState && appState.playerState && !appState.playerState.isLoading && appState.playerState.accessToken.length == 0) {
                const url = '/api/token?personalId=' + personalId;
                fetch(url)
                    .then(response => response.json() as Promise<PlayerState>)
                    .then(data => {
                        dispatch({ type: 'RECEIVE_PLAYER_ACCESS_TOKEN', personalId: data.personalId, accessToken: data.accessToken, expiry: data.expiry });
                    });

                dispatch({ type: 'REQUEST_PLAYER_ACCESS_TOKEN', personalId: personalId });
            }
        } catch ({ status, message }) {
            console.log(message);
        }
    }
}

const unloadedState: PlayerState = { isLoading: false, personalId: 0, accessToken: "", expiry: 0 };

export const reducer: Reducer<PlayerState> = (state: PlayerState | undefined, incomingAction: Action): PlayerState => {
     if (state === undefined) {
        return unloadedState;
    }

    const action = incomingAction as KnownAction;
    console.log(action);

    switch (action.type) {
        case 'REQUEST_PLAYER_ACCESS_TOKEN':
             return {
                isLoading: true,
                personalId: state.personalId,
                accessToken: state.accessToken,
                expiry: state.expiry
            };
        case 'RECEIVE_PLAYER_ACCESS_TOKEN':
            return {
                isLoading: false,
                personalId: action.personalId,
                accessToken: action.accessToken,
                expiry: action.expiry
            };
            break;
    }

    return state;
};