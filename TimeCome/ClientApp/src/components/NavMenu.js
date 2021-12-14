import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import { LoginMenu } from './api-authorization/LoginMenu';
import './NavMenu.css';
import authService from './api-authorization/AuthorizeService';

export class NavMenu extends Component {
  static displayName = NavMenu.name;

  constructor (props) {
    super(props);

    this.toggleNavbar = this.toggleNavbar.bind(this);
    this.state = {
      collapsed: true,
      isAuthenticated: false
    };
  }
     
  toggleNavbar () {
    this.setState({
      collapsed: !this.state.collapsed
    });
  }
    
  componentDidMount() {
      this.UserAuthenticated();
  }
    
  async UserAuthenticated() {
    let isAuth = await authService.isAuthenticated();
    this.setState({
      isAuthenticated: isAuth
    });
  }

  AuthenticatedComponents() {
    if (this.state.isAuthenticated) {
      return (
        <NavItem>
          <NavLink tag={Link} className="text-light" to="/todolist">TodoList</NavLink>
        </NavItem>
      );
    }
  }

  render () {
    return (
      <header>
        <Navbar className="navbar-expand-md border-bottom box-shadow" light>
          <Container fluid>
            <NavbarBrand tag={Link} to="/" className="text-light">TimeCome</NavbarBrand>
            <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
            <Collapse className="d-md-inline-flex flex-md-row-reverse" isOpen={!this.state.collapsed} navbar>
              <ul className="navbar-nav flex-grow">
                <NavItem>
                  <NavLink tag={Link} className="text-light" to="/">Home</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink tag={Link} className="text-light" to="/about">About</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink tag={Link} className="text-light" to="/contacts">Contacts</NavLink>
                </NavItem>
                {this.AuthenticatedComponents()}
                <LoginMenu>
                </LoginMenu>
              </ul>
            </Collapse>
          </Container>
        </Navbar>
      </header>
    );
  }
}
