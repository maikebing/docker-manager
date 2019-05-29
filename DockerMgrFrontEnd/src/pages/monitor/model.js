import { getAllContainers, AddServer, DeleteServer } from './service';
import { getAuthority, getUserInfo } from '@/utils/authority';

export default {
  namespace: 'container',

  state: {
    serversList: undefined, 
    containersList: undefined,
  },

  effects: {
    *get({ payload }, { call, put }) {
      const response = yield call(getAllContainers, payload);
      yield put({
        type: 'save',
        payload: {
          servers: response.data.servers,
          containers: response.data.containers,
        },
      });
    },

    *addNewServer({ payload: p }, { call, put }) {
      const r = yield call(AddServer, p);
      const { id } = getUserInfo();
      const response = yield call(getAllContainers, {id: id});
      yield put({
        type: 'save',
        payload: {
          servers: response.data.servers,
          containers: response.data.containers,
        },
      });
    },

    *delete({ payload }, { call, put }) {
      const r = yield call(DeleteServer, payload);
      const { id } = getUserInfo();
      const response = yield call(getAllContainers, { id: id });
      yield put({
        type: 'save',
        payload: {
          servers: response.data.servers,
          containers: response.data.containers,
        },
      });
    },
  },

  reducers: {
    save(state, { payload: { servers: serversList, containers: containersList } }) {
      return { ...state, serversList, containersList};
    },
  },

  subscriptions: {
    setup({ dispatch, history }) {
      return history.listen(({ pathname, query }) => {
        let token = getAuthority();
        if ((pathname === '/monitor' || pathname === '/terminal') && token !== 'null') {
          const { id } = getUserInfo();
          dispatch({ type: 'get', payload: {id}});
        }
      });
    },
  },

};
