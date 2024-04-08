import { FC } from 'react'
import { Header } from './components/header/Header'
import { Main } from './components/main/Main'
import { Footer } from './components/footer/Footer'

import './App.css'
import { createBrowserRouter, RouterProvider } from 'react-router-dom'
import { AuctionPage } from './components/auction-page/AuctionPage'
import { AuctionContextProvider } from './contexts/AuctionsContext'
import { AuctionsListContainer } from './components/auction-page/auction-list/AuctionsListContainer'

const router = createBrowserRouter([
  {
    path: "/",
    element: <AuctionsListContainer />
  },
  {
    path: "/auction/:id",
    element: <AuctionPage />
  }
])

const App: FC = () => {
  return (
    <AuctionContextProvider>
      <Header />
      <Main>
        <RouterProvider router={router} />
      </Main>
      <Footer />
    </AuctionContextProvider>
  );
}

export default App;
