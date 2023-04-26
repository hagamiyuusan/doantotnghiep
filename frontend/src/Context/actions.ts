const ROOT_URL = 'https://secret-hamlet-03431.herokuapp.com'

export async function loginUser(dispatch: any, loginPayload: any) {
  const requestOptions = {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(loginPayload)
  }

  try {
    dispatch({ type: 'REQUEST_LOGIN' })
    const response = await fetch(`${ROOT_URL}/login`, requestOptions)
    const data = await response.json()

    if (data.user) {
      dispatch({ type: 'LOGIN_SUCCESS', payload: data })
      localStorage.setItem('currentUser', JSON.stringify(data))
      return data
    }

    dispatch({ type: 'LOGIN_ERROR', error: data.errors[0] })
    console.log(data.errors[0])
    return
  } catch (error) {
    dispatch({ type: 'LOGIN_ERROR', error: error })
    console.log(error)
  }
}

export async function logout(dispatch: any) {
  dispatch({ type: 'LOGOUT' })
  localStorage.removeItem('currentUser')
  localStorage.removeItem('token')
}
